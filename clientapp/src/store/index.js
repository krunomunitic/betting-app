import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        competitionsBySports: [],
        fixturesByCompetition: [],
    },
    getters: {
        competitionsBySports: state => {
            return state.competitionsBySports
        },
        fixturesByCompetition: state => {
            return state.fixturesByCompetition
        },
    },
    mutations: {
        SET_COMPETITIONBYSPORTS(state, competitionsBySports) {
            state.competitionsBySports = competitionsBySports
        },
        SET_FIXTURESBYCOMPETITION(state, fixturesByCompetition) {
            state.fixturesByCompetition = fixturesByCompetition
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
    },
})
