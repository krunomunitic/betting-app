export const mutations = {
    SET_FIXTURES(state, fixtures) {
        let copyFixtures = [...fixtures]
        state.fixtures = copyFixtures
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
