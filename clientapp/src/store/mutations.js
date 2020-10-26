export const mutations = {
    SET_COMPETITIONBYSPORTS(state, competitionsBySports) {
        state.competitionsBySports = competitionsBySports
    },
    SET_FIXTURES(state, fixtures) {
        // problem with re-rendering not triggered fix
        if (fixtures && fixtures.fixturesByCompetition) {
            const temp = [...fixtures.fixturesByCompetition]
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
}
