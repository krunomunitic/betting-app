export const getters = {
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
}
