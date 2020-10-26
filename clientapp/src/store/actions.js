import router from '../router/'
import axios from 'axios';

export const actions = {
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
            router.push({ name: 'fixtures' });
        }).catch(error => {
            console.log(error)
        })
    },
    updateFixturesWithStatus({ commit, getters }, betOnFixture) {
        const fixtures = getters.fixtures

        fixtures.forEach(fixtureGroup => {
            const fixture = fixtureGroup.fixtures.find(fixture => fixture.id === betOnFixture.fixtureId)
            if (fixture) {
                fixture.hasBet = false
                for (const [oddsName, odds] of Object.entries(fixture.odds)) {
                    if (betOnFixture.fixturesGroupName === fixtureGroup.name && oddsName == betOnFixture.oddsType) {
                        odds.betted = true
                        fixture.hasBet = true
                    } else {
                        odds.betted = false
                    }
                }
            }
        })

        commit('SET_FIXTURES', fixtures)
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
            betExist.fixturesGroupName = bet.fixturesGroupName

            // TODO: fix this hardcoded name
            if (bet.fixturesGroupName === 'Special Offers') {
                betExist.special = true
            } else {
                betExist.special = false
            }
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
    async betOnTicket({ commit, getters, dispatch }, stake) {
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
            commit('SET_TICKET', {})
            console.log(response)
        }).catch(e => {
            console.log(e)
        })
    }
}