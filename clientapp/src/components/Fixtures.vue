<template>
    <div>
        <div v-for='fixtureCompetition in fixturesByCompetition' :key='fixtureCompetition.competitionName'>
            <div>{{fixtureCompetition.competitionName}}</div>
            <div v-for="fixture in fixtureCompetition.fixtures" :key='fixture.id'>
                {{fixture.data}} {{fixture.homeTeamName}} - {{fixture.awayTeamName}} {{fixture.result}}
                <b-button-group v-for="odds in fixture.odds" :key="odds.name">
                    <b-button v-if="odds.name === '1'">{{odds.value}}</b-button>
                    <b-button v-if="odds.name === '2'">{{odds.value}}</b-button>
                    <b-button v-if="odds.name === 'X'">{{odds.value}}</b-button>
                    <b-button v-if="odds.name === '12'">{{odds.value}}</b-button>
                    <b-button v-if="odds.name === 'X1'">{{odds.value}}</b-button>
                    <b-button v-if="odds.name === 'X2'">{{odds.value}}</b-button>
                </b-button-group>
            </div>
        </div>
    </div>
</template>

<script>
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
        }
    }
</script>

<style scoped>
</style>
