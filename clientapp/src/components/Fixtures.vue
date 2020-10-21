<template>
    <div>
        <div v-for='fixtureCompetition in fixturesByCompetition' :key='fixtureCompetition.competitionName'>
            <b-row align-h="between">
                <b-col cols="5">
                    <div>{{fixtureCompetition.competitionName}}</div>
                </b-col>
                <b-col cols="7">
                </b-col>
            </b-row>
            <div v-for="fixture in fixtureCompetition.fixtures" :key='fixture.id'>
                <b-row align-h="between" :style="[fixture.special && `background-color: gray;`]">
                    <b-col cols="5">
                        {{fixture.date}} {{fixture.homeTeamName}} - {{fixture.awayTeamName}} {{fixture.result}}
                    </b-col>
                    <b-col cols="7">
                        <b-row>
                            <b-button squared disable="!fixture.odds['1'] || !fixture.odds['1'].value"
                                      @click="addBet(fixture, fixture.odds['1'], '1');
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id, '1');"
                                      :pressed="fixture.odds['1'].betted && fixture.hasBet">
                                {{fixture.odds['1'].value.toFixed(2)}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['X'] || !fixture.odds['X'].value"
                                      @click="addBet(fixture, fixture.odds['X'], 'X');
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id, 'X');"
                                      :pressed="fixture.odds['X'].betted && fixture.hasBet">
                                {{fixture.odds['X'].value.toFixed(2)}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['2'] || !fixture.odds['2'].value"
                                      @click="addBet(fixture, fixture.odds['2'], '2');
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id, '2');"
                                      :pressed="fixture.odds['2'].betted && fixture.hasBet">
                                {{fixture.odds['2'].value.toFixed(2)}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['12'] || !fixture.odds['12'].value"
                                      @click="addBet(fixture, fixture.odds['12'], '12');
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id, '12');"
                                      :pressed="fixture.odds['12'].betted && fixture.hasBet">
                                {{fixture.odds['12'].value.toFixed(2)}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['X1'] || !fixture.odds['X1'].value"
                                      @click="addBet(fixture, fixture.odds['X1'], 'X1');
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id, 'X1');"
                                      :pressed="fixture.odds['X1'].betted && fixture.hasBet">
                                {{fixture.odds['X1'].value.toFixed(2)}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['X2'] || !fixture.odds['X2'].value"
                                      @click="addBet(fixture, fixture.odds['X2'], 'X2');
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id, 'X2');"
                                      :pressed="fixture.odds['X2'].betted && fixture.hasBet">
                                {{fixture.odds['X2'].value.toFixed(2)}}
                            </b-button>
                            <b-button squared v-if="fixture.hasBet"
                                      @click="removeBet(fixture.id);
                                      updateFixturesByCompetition(fixtureCompetition.competitionName, fixture.id);">
                                X
                            </b-button>
                        </b-row>
                    </b-col>
                </b-row>
            </div>
        </div>
        <b-button squared v-if="ticket && ticket.bets && ticket.bets.length"
                  @click="placeBet()">
            Place Bet
        </b-button>
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
            this.$store.dispatch('getFixturesByCompetition')
        },
        computed: {
            ...mapGetters(['fixturesByCompetition', 'ticket'])
        },
        methods: {
            // ...mapActions(['addBet', './store/index']),
            addBet({ id, date, homeTeamName, awayTeamName }, odds, oddsType) {
                this.$store.dispatch('addBet', { fixtureId: id, date, homeTeamName, awayTeamName, odds, oddsType })
            },
            updateFixturesByCompetition(competitionName, fixtureId, oddsType) {
                this.$store.dispatch('updateFixturesByCompetition', { competitionName, fixtureId, oddsType })
            },
            removeBet(fixtureId) {
                this.$store.dispatch('removeBet', fixtureId)
            },
            betOnTicket() {
                this.$store.dispatch('betOnTicket')
            },
            placeBet() {
                this.$router.push({ name: 'ticket' });
            },
        },
    }
</script>

<style scoped>
</style>
