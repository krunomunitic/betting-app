<template>
    <div>
        <div v-for='bet in ticket.bets' :key='bet.fixtureId'>
            <b-row align-h="between">
                <b-col cols="7">
                    {{bet.date}} {{bet.homeTeamName}} - {{bet.awayTeamName}}
                </b-col>
                <b-col cols="2">
                    <b-button squared disable="true">
                        {{bet.oddsType}} - {{bet.odds.value.toFixed(2)}}
                    </b-button>
                </b-col>
            </b-row>
        </div>
        <b-row>
            <b-col sm="5">
                <b-form-input type="number" :value="stake" @input="updateReturns" min="0" placeholder="Stake 12.34$"></b-form-input>
            </b-col>
        </b-row>
        <b-row>
            <b-col sm="6">
                Estimated returns: {{(returns).toFixed(2)}}
                Tax: {{(tax).toFixed(2)}}
                Returns After Tax: {{returnsAfterTax.toFixed(2)}}
            </b-col>
            <b-col sm="6">
                <b-button squared @click="goBack()">
                    Go back
                </b-button>
                <b-button squared @click="betOnTicket()">
                    Bet On Ticket
                </b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    // import { mapActions } from 'vuex'
    // import { mapGetters } from 'vuex'

    export default {
        data() {
            return {
                stake: null,
                tax: 0,
                returns: 0,
                returnsAfterTax: 0
            }
        },
        computed: {
            ticket() {
                const ticket = this.$store.getters.ticket;
                if (!ticket || !ticket.bets || !ticket.bets.length) {
                    this.goBack()
                }

                return ticket
            },
            oddsCalc() {
                // ticket already got
                if (!this.ticket || !this.ticket.bets || !this.ticket.bets.length) {
                    this.goBack()
                }

                let oddsCalc = 1;
                this.ticket.bets.forEach(bet => {
                    oddsCalc *= bet.odds.value
                })
                return oddsCalc
            }
        },
        methods: {
            // ...mapActions(['addBet', 'store']),
            betOnTicket() {
                this.$store.dispatch('betOnTicket', Number(this.stake))
                // this.$store.dispatch('addBet', { fixtureId, odds, oddsType })
            },
            goBack() {
                this.$router.push({ name: 'fixtures' });
            },
            updateReturns(value) {
                this.stake = value
                this.returns = this.stake * this.oddsCalc
                this.tax = this.returns * 0.05
                this.returnsAfterTax = this.returns - this.tax
            }
        },
    }</script>

<style scoped>
</style>
