<template>
    <div class="fixtures">
        <b-row>
            <b-col cols="12">
                <h2>Ticket:</h2>
            </b-col>
            <b-col cols="12">
                <div class="fixtures__competition__fixture" v-for='bet in ticket.bets' :key='bet.fixtureId'>
                    <b-row>
                        <b-col cols="7">
                            {{bet.date | moment}} {{bet.homeTeamName}} - {{bet.awayTeamName}}
                        </b-col>
                        <b-col cols="5">
                            <div>
                                {{bet.oddsType}} - {{bet.odds.value.toFixed(2)}}
                            </div>
                        </b-col>
                    </b-row>
                </div>
            </b-col>
        </b-row>
        <b-row>
            <b-col offset-sm="4" sm="3">
                <b-form-input type="number" :value="stake" @input="updateReturns" min="0" placeholder="Stake 12.34$"></b-form-input>
            </b-col>
            <b-col sm="5" class="ticket__bet-calc">
                <span>Estimated returns: {{(returns).toFixed(2)}}</span>
                <span>Tax: {{(tax).toFixed(2)}}</span>
                <span>Returns After Tax: {{returnsAfterTax.toFixed(2)}}</span>
            </b-col>
        </b-row>
        <b-row>
            <b-col offset-sm="4" sm="3" class="ticket__buttons">
                <b-button @click="betOnTicket()" class="ticket__buttons__bet">
                    Bet On Ticket
                </b-button>
                <b-button variant="outline-primary" class="ticket__buttons__go-back" @click="goBack()">
                    Go back
                </b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import moment from 'moment'

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
            betOnTicket() {
                this.$store.dispatch('betOnTicket', Number(this.stake))
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
        filters: {
            moment(date) {
                return moment(date).format('YYYY-MM-DD HH:mm');
            },
        }
    }</script>

<style scoped>
</style>
