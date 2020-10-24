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
                        {{bet.fixtureDate}} {{bet.homeTeamName}} - {{bet.awayTeamName}} {{bet.result}}
                    </b-col>
                    <b-col cols="2">
                        {{bet.oddsName}} - {{bet.oddsValue}}
                    </b-col>
                </b-row>
            </div>
            <b-row align-h="between">
                <b-col cols="3">
                    Stakes: {{ ticket.stake }}
                </b-col>
                <b-col cols="3">
                    Odds Total: {{ ticket.oddsCalc.toFixed(2) }}
                </b-col>
                <b-col cols="3">
                    Win: {{ (ticket.stake * ticket.oddsCalc ).toFixed(2) }}
                </b-col>
            </b-row>
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
