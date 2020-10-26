<template>
    <div>
        <div class="fixtures" v-for='ticket in tickets' :key='ticket.id'>
            <b-row>
                <b-col cols="12">
                    <h2>Ticket:</h2>
                </b-col>
                <b-col cols="12">
                    <div class="fixtures__competition__fixture" v-for="bet in ticket.bets" :key='bet.id'>
                        <b-row align-h="between">
                            <b-col cols="7">
                                {{bet.fixtureDate | moment}} {{bet.homeTeamName}} - {{bet.awayTeamName}} {{bet.result}}
                            </b-col>
                            <b-col cols="5">
                                {{bet.oddsName}} - {{bet.oddsValue}}
                            </b-col>
                        </b-row>
                    </div>
                </b-col>
                <b-col offset-sm="7" sm="5" class="ticket__bet-calc">
                    <span>Stakes: {{ ticket.stake }}</span>
                    <span>Odds Total: {{ ticket.oddsCalc.toFixed(2) }}</span>
                    <span>Win: {{ (ticket.stake * ticket.oddsCalc ).toFixed(2) }}</span>
                </b-col>
            </b-row>
        </div>
    </div>
</template>

<script>
    import moment from 'moment'
    import { mapGetters } from 'vuex'

    export default {
        mounted() {
            this.$store.dispatch('getTickets')
        },
        computed: {
            ...mapGetters(['tickets'])
        },
        filters: {
            moment(date) {
                return moment(date).format('YYYY-MM-DD HH:mm');
            },
        }
    }</script>

<style scoped>
</style>
