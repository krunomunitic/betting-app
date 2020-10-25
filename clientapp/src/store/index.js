import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';
import router from '../router/'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        competitionsBySports: [],
        fixturesByCompetition: [],
        fixturesByCompetitionSpecial: [],
        ticket: {},
        balance: 0,
        tickets: []
    },
    getters: {
        competitionsBySports: state => {
            return state.competitionsBySports
        },
        fixturesByCompetition: state => {
            return state.fixturesByCompetition
        },
        fixturesByCompetitionSpecial: state => {
            return state.fixturesByCompetitionSpecial
        },
        ticket(state) {
            return state.ticket;
        },
        balance(state) {
            return state.balance
        },
        tickets(state) {
            return state.tickets
        }
    },
    mutations: {
        SET_COMPETITIONBYSPORTS(state, competitionsBySports) {
            state.competitionsBySports = competitionsBySports
        },
        SET_FIXTURES(state, fixtures) {
            // problem with re-rendering not triggered fix
            if (fixtures && fixtures.fixturesByCompetition) {
                const temp = [ ...fixtures.fixturesByCompetition ]
                state.fixturesByCompetition = temp
            }

            // problem with re-rendering not triggered fix
            if (fixtures && fixtures.fixturesByCompetitionSpecial) {
                const temp = [...fixtures.fixturesByCompetitionSpecial]
                state.fixturesByCompetitionSpecial = temp
            }
        },
        SET_TICKET(state, ticket) {
            // problem with re-rendering not triggered fix
            let copyTicket = { ...ticket }
            state.ticket = copyTicket
        },
        SET_BALANCE(state, balance) {
            state.balance = balance
        },
        SET_TICKETS(state, tickets) {
            state.tickets = tickets
        }
    },
    actions: {
        getCompetitionsBySports({ commit }) {
            axios.get('/api/competition').then(({ data }) => {
                commit('SET_COMPETITIONBYSPORTS', data)
            }) 
        },
        getFixtures({ commit }) {
            axios.get('/api/fixture').then(({ data }) => {
                commit('SET_FIXTURES', data)
            })
        },
        getTickets({ commit }) {
            axios.get('/api/ticket').then(({ data }) => {
                commit('SET_TICKETS', data)
            })
        },
        async getWalletBalance({ commit }) {
            await axios.get('/api/wallet').then(({ data }) => {
                commit('SET_BALANCE', data)
            })
        },
        updateBalance({ commit }, balance) {
            axios.post('/api/wallet', { balance }
            ).then(response => {
                commit('SET_BALANCE', balance)
                console.log(response)
            }).catch(error => {
                console.log(error)
            })
        },
        updateFixturesWithStatus({ commit, getters }, betOnFixture) {
            const fixturesByCompetition = getters.fixturesByCompetition

            const competition = fixturesByCompetition.find(competition =>
                competition.competitionName === betOnFixture.competitionName)

            if (!competition) {
                console.log('error - No competition')
                return;
            }

            const fixture = competition.fixtures.find(fixture =>
                fixture.id === betOnFixture.fixtureId)

            if (!fixture) {
                console.log('error - No fixture')
                return;
            }

            if (betOnFixture.oddsType && !betOnFixture.special) {
                fixture.hasBet = true
                for (const [oddsName, odds] of Object.entries(fixture.odds)) {
                    odds.betted = oddsName === betOnFixture.oddsType
                }
            } else {
                fixture.hasBet = false
                for (const odds of Object.entries(fixture.odds)) {
                    odds.betted = false
                }
            }
            commit('SET_FIXTURES', { fixturesByCompetition })

            // set for special fixtures

            const fixturesByCompetitionSpecial = getters.fixturesByCompetitionSpecial

            const competitionSpecial = fixturesByCompetitionSpecial.find(competition =>
                competition.competitionName === betOnFixture.competitionName)

            if (!competitionSpecial) {
                console.log('error - no competition in special')
                return;
            }

            const fixtureSpecial = competitionSpecial.fixtures.find(fixture =>
                fixture.id === betOnFixture.fixtureId)

            if (!fixtureSpecial) {
                console.log('log - no fixture in special')
                return;
            }

            if (betOnFixture.oddsType && betOnFixture.special) {
                fixtureSpecial.hasBet = true
                for (const [oddsName, odds] of Object.entries(fixtureSpecial.odds)) {
                    odds.betted = oddsName === betOnFixture.oddsType
                }
            } else {
                fixtureSpecial.hasBet = false
                for (const odds of Object.entries(fixtureSpecial.odds)) {
                    odds.betted = false
                }
            }

            commit('SET_FIXTURES', { fixturesByCompetitionSpecial })

        },
        addBet({ commit, getters }, bet) {
            const ticket = getters.ticket

            if (!ticket.bets || !ticket.bets.length) {
                ticket.bets = [bet]
            }

            const betExist = ticket.bets.find(b => b.fixtureId === bet.fixtureId);
            if (betExist) {
                betExist.odds = bet.odds
                betExist.oddsType = bet.oddsType
                betExist.special = bet.special
            } else {
                ticket.bets.push(bet)
            }

            commit('SET_TICKET', ticket)
        },
        removeBet({ commit, getters }, fixtureId) {
            const ticket = getters.ticket
            if (!ticket.bets || !ticket.bets.length) {
                console.log("error")
            }

            const betIndex = ticket.bets.findIndex(bet => bet.fixtureId === fixtureId)

            if (betIndex === '-1') {
                console.log("error - no bet")
                return;
            }

            ticket.bets.splice(betIndex, 1)

            commit('SET_TICKET', ticket)
        },
        validateTicket({ getters }) {
            const ticket = getters.ticket
            if (!ticket || !ticket.bets || !ticket.bets.length) {
                console.log("error - no bets")
                return;
            }

            const numberOfSpecialFixtures = ticket.bets.filter(bet => bet.special).length
            if (numberOfSpecialFixtures > 1) {
                console.log("error - only one special allowed")
                return;
            }

            if (numberOfSpecialFixtures === 1) {
                // check if there are at least 5 other bets
                if (ticket.bets.length < 6) {
                    console.log("error - less than 5 other bets")
                    return;
                }

                // check if the odds of other bets are over or 1.1
                const numberOfValidOdds = ticket.bets.filter(bet => bet.odds && bet.odds.value >= 1.1 && !bet.special).length
                if (numberOfValidOdds < ticket.bets.length - 1) {
                    console.log("error - not all bets are valid")
                    return;
                }
            }

            router.push({ name: 'ticket' });
        },
        async betOnTicket({ getters, dispatch }, stake) {
            const ticket = getters.ticket

            if (!ticket || !ticket.bets || !ticket.bets.length) {
                console.log("error - no bets")
                return;
            }

            if (Number(this.stake) < 0) {
                console.log("error - stake must be higher than 0")
                return;
            }

            await dispatch('getWalletBalance')
            const balance = getters.balance

            if (stake >= balance) {
                console.log("error - not enough balance")
                return;
            }

            const formattedTicket = {
                Stake: stake,
                Bets: ticket.bets.map(bet => ({
                    FixtureId: bet.fixtureId,
                    OddsId: bet.odds.id
                }))
            }

            axios.post('/api/ticket', formattedTicket
            ).then(response => {
                console.log(response)
            }).catch(e => {
                console.log(e)
            })
        }
    },
})
