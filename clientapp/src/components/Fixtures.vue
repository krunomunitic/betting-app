<template>
    <div>
        <div v-for='(fixtures, competition) in fixturesByCompetition' :key='competition'>
            <div>{{competition}}</div>
            <div v-for="fixture in fixtures" :key='fixture.id'>
                {{fixture.data}} {{fixture.homeTeamName}} - {{fixture.awayTeamName}} {{fixture.result}}
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
