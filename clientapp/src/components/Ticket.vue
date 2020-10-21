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
                <b-form-input type="number" v-model="stake" min="0" placeholder="Stake 12.34$"></b-form-input>
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
                stake: null
            }
        },
        mounted() {
            // this.$store.dispatch('getFixturesByCompetition')
        },
        computed: {
            ticket() {
                const ticket = this.$store.getters.ticket;
                if (!ticket || !ticket.bets || !ticket.bets.length) {
                    this.goBack()
                }
                return ticket
            }
        },
        methods: {
            // ...mapActions(['addBet', './store/index']),
            betOnTicket() {
                this.$store.dispatch('betOnTicket', Number(this.stake))
                // this.$store.dispatch('addBet', { fixtureId, odds, oddsType })
            },
            goBack() {
                this.$router.push({ name: 'fixtures' });
            }
        },
    }
</script>

<style scoped>
</style>
