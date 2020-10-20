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
        getTicket(state) {
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
            state.ticket = ticket
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
        addBet({ commit, getters }, bet) {
            const ticket = getters.getTicket

            // TODO: Validate if bet can be placed on this ticket
            if (ticket.bets && ticket.bets.length) {
                ticket.bets.push(bet)
            }
            else {
                ticket.bets = [bet]
            }
            commit('SET_TICKET', ticket)
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

            for (const [oddsName, odds] of Object.entries(fixture.odds)) {
                odds.betted = oddsName === betOnFixture.oddsType
            }

            commit('SET_FIXTURESBYCOMPETITION', fixturesByCompetition)
            // TODO update fixtures if special exist
        }
    },
})
