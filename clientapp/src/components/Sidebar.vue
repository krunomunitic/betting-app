<template>
    <div style="min-height: 100vh; float:left;">
        <b-nav vertical class="w-15" v-for='(leagues, sport) in competitionsBySports' :key='sport'>
            <div> {{sport}} </div>
            <div v-for='league in leagues' :key='league.id'>
                <b-nav-item>{{league.name}}</b-nav-item>
            </div>
        </b-nav>
    </div>
</template>

<script>
//import { mapGetters } from 'vuex'
export default {
    data() {
        return {};
        },
        mounted() {
            this.$store.dispatch('getCompetitionsBySports')
        },
        computed: {
            competitionsBySports() {
                return this.$store.state.competitionsBySports
            },
        }
    /*
    computed: {
        ...mapGetters({
            competitionsBySports: 'getCompetitionsBySports'
        })
    }*/
}
</script>

<style scoped>
</style>
