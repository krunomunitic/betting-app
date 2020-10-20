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
                <b-row align-h="between">
                    <b-col cols="5">
                        {{fixture.data}} {{fixture.homeTeamName}} - {{fixture.awayTeamName}} {{fixture.result}}
                    </b-col>
                    <b-col cols="7">
                        <b-row>
                            <b-button squared disable="!fixture.odds['1'] || !fixture.odds['1'].value"
                                      @click="addBet(fixture.id, fixture.odds['1'].value, '1');
                                      updateFixtureInfo(fixture.id, '1', fixtureCompetition.competitionName);"
                                      :pressed="fixture.odds['1'].betted">
                                {{fixture.odds['1'].value}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['X'] || !fixture.odds['X'].value"
                                      @click="addBet(fixture.id, fixture.odds['X'].value, 'X');
                                      updateFixtureInfo(fixture.id, 'X', fixtureCompetition.competitionName);"
                                      :pressed="fixture.odds['X'].betted">
                                {{fixture.odds['X'].value}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['2'] || !fixture.odds['2'].value"
                                      @click="addBet(fixture.id, fixture.odds['2'].value, '2');
                                      updateFixtureInfo(fixture.id, '2', fixtureCompetition.competitionName);"
                                      :pressed="fixture.odds['2'].betted">
                                {{fixture.odds['2'].value}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['12'] || !fixture.odds['12'].value"
                                      @click="addBet(fixture.id, fixture.odds['12'].value, '12');
                                      updateFixtureInfo(fixture.id, '12', fixtureCompetition.competitionName);"
                                      :pressed="fixture.odds['12'].betted">
                                {{fixture.odds['12'].value}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['X1'] || !fixture.odds['X1'].value"
                                      @click="addBet(fixture.id, fixture.odds['X1'].value, 'X1');
                                      updateFixtureInfo(fixture.id, 'X1', fixtureCompetition.competitionName);"
                                      :pressed="fixture.odds['X1'].betted">
                                {{fixture.odds['X1'].value}}
                            </b-button>
                            <b-button squared disable="!fixture.odds['X2'] || !fixture.odds['X2'].value"
                                      @click="addBet(fixture.id, fixture.odds['X2'].value, 'X2');
                                      updateFixtureInfo(fixture.id, 'X2', fixtureCompetition.competitionName);"
                                      :pressed="fixture.odds['X2'].betted">
                                {{fixture.odds['X2'].value}}
                            </b-button>
                        </b-row>
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
            this.$store.dispatch('getFixturesByCompetition')
        },
        computed: {
            ...mapGetters(['fixturesByCompetition'])
        },
        methods: {
            // ...mapActions(['addBet', './store/index']),
            addBet(fixtureId, odds, oddsType) {
                this.$store.dispatch('addBet', { fixtureId, odds, oddsType })
            },
            updateFixtureInfo(fixtureId, oddsType, competitionName) {
                this.$store.dispatch('updateFixturesByCompetition', { fixtureId, oddsType, competitionName })
            }
        },
    }
</script>

<style scoped>
</style>
