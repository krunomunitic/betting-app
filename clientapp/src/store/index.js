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
            state.fixturesByCompetition = fixturesByCompetition
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
        }
    },
})
