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
                            <div v-for="odds in fixture.odds" :key="odds.name">
                                <b-button v-if="odds.name === '1'" @click="addBet(fixture.id, odds)">{{odds.value}}</b-button>
                                <b-button v-if="odds.name === '2'" @click="addBet(fixture.id, odds)" >{{odds.value}}</b-button>
                                <b-button v-if="odds.name === 'X'" @click="addBet(fixture.id, odds)">{{odds.value}}</b-button>
                                <b-button v-if="odds.name === '12'" @click="addBet(fixture.id, odds)">{{odds.value}}</b-button>
                                <b-button v-if="odds.name === 'X1'" @click="addBet(fixture.id, odds)">{{odds.value}}</b-button>
                                <b-button v-if="odds.name === 'X2'" @click="addBet(fixture.id, odds)">{{odds.value}}</b-button>
                            </div>
                        </b-row>
                    </b-col>
                </b-row>
            </div>
        </div>
    </div>
</template>

<script>
    // import { mapActions } from 'vuex'

    export default {
        data() {
            return {};
        },
        mounted() {
            this.$store.dispatch('getFixturesByCompetition')
        },
        computed: {
            fixturesByCompetition() {
                return this.$store.state.fixturesByCompetition
            },
        },
        methods: {
            addBet(fixtureId, odds) {
                this.$store.dispatch('addBet', { fixtureId: fixtureId, odds })
                // TODO: extra bets on this fixture are disabled
            }
            // ...mapActions(['addBet', './store/index']),
        }
    }
</script>

<style scoped>
</style>
