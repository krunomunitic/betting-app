<template>
    <div>
        <b-row align-h="between">
            <b-col cols="12">
                Tickets
            </b-col>
        </b-row>
        <div v-for='ticket in tickets' :key='ticket.id'>
            {{ ticket.id }}
            <div v-for="bet in ticket.bets" :key='bet.id'>
                <b-row align-h="between">
                    <b-col cols="8">
                        {{bet.fixture.date}} {{bet.fixture.homeTeam.name}} - {{bet.fixture.awayTeam.name}} {{bet.fixture.result}}
                    </b-col>
                    <b-col cols="2">
                        {{bet.odds.name}} - {{bet.odds.value}}
                    </b-col>
                </b-row>
            </div>
        </div>
    </div>
</template>

<script>
    // import { mapActions } from 'vuex'
    import { mapGetters } from 'vuex'

    export default {
        data() {
            return {};
        },
        mounted() {
            this.$store.dispatch('getTickets')
        },
        computed: {
            ...mapGetters(['tickets'])
        },
    }
</script>

<style scoped>
</style>
