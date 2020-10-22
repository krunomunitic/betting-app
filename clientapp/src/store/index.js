import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        competitionsBySports: [],
        fixturesByCompetition: [],
        ticket: {},
        balance: 0,
    },
    getters: {
        competitionsBySports: state => {
            return state.competitionsBySports
        },
        fixturesByCompetition: state => {
            return state.fixturesByCompetition
        },
        ticket(state) {
            return state.ticket;
        },
        balance(state) {
            return state.balance
        }
    },
    mutations: {
        SET_COMPETITIONBYSPORTS(state, competitionsBySports) {
            state.competitionsBySports = competitionsBySports
        },
        SET_FIXTURESBYCOMPETITION(state, fixturesByCompetition) {
            // problem with re-rendering not triggered fix
            let copyFBC = [ ...fixturesByCompetition ]
            state.fixturesByCompetition = copyFBC
        },
        SET_TICKET(state, ticket) {
            // problem with re-rendering not triggered fix
            let copyTicket = { ...ticket }
            state.ticket = copyTicket
        },
        SET_BALANCE(state, balance) {
            state.balance = balance
        }
    },
    actions: {
        getCompetitionsBySports({ commit }) {
            axios.get('/api/competition').then(({ data }) => {
                commit('SET_COMPETITIONBYSPORTS', data)
            }) 
        },
        getFixturesByCompetition({ commit }) {
            axios.get('/api/fixture').then(({ data }) => {
                commit('SET_FIXTURESBYCOMPETITION', data)
            })
        },
        getWalletBalance({ commit }) {
            axios.get('/api/wallet').then(({ data }) => {
                commit('SET_BALANCE', data)
            })
        },
        updateFixturesByCompetition({ commit, getters }, betOnFixture) {
            const fixturesByCompetition = getters.fixturesByCompetition

            const competition = fixturesByCompetition.find(competition =>
                competition.competitionName === betOnFixture.competitionName)

            if (!competition) {
                console.log('error')
                return;
            }

            const fixture = competition.fixtures.find(fixture =>
                fixture.id === betOnFixture.fixtureId)

            if (!fixture) {
                console.log('error')
                return;
            }

            if (betOnFixture.oddsType) {
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

            commit('SET_FIXTURESBYCOMPETITION', fixturesByCompetition)
            // TODO update fixtures if special exist
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
                console.log("error")
                return;
            }

            ticket.bets.splice(betIndex, 1)

            commit('SET_TICKET', ticket)
        },
        betOnTicket({ getters, dispatch }, stake) {
            dispatch('getWalletBalance')

            const ticket = getters.ticket
            const balance = getters.balance

            if (stake > balance) { console.log("error")
                return;
            }

            if (!ticket || !ticket.bets || !ticket.bets.length) {
                console.log("error")
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
        },
        updateBalance({ commit }, balance) {
            axios.post('/api/wallet', { balance }
            ).then(response => {
                commit('SET_BALANCE', balance)
                console.log(response)
            }).catch(e => {
                console.log(e)
            })
        }
    },
})
