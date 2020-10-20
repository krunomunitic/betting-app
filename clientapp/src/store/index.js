import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        competitionsBySports: [],
        fixturesByCompetition: [],
        ticket: {},
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
        updateFixturesByCompetition({ commit, getters }, betOnFixture) {
            const fixturesByCompetition = getters.fixturesByCompetition

            const competition = fixturesByCompetition.find(competition =>
                competition.competitionName === betOnFixture.competitionName)

            if (!competition) {
                console.log('error')
            }

            const fixture = competition.fixtures.find(fixture =>
                fixture.id === betOnFixture.fixtureId)

            if (!fixture) {
                console.log('error')
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
            }

            ticket.bets.splice(betIndex, 1)

            commit('SET_TICKET', ticket)
        },
        betOnTicket() {

        }
    },
})
