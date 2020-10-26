<template>
    <div style="min-height: 100vh; float:left;">
        <b-nav vertical class="w-15" v-for='competitionSport in competitionsBySports' :key='competitionSport.sportName'>
            <div> {{competitionSport.sportName}} </div>
            <div v-for='competition in competitionSport.competitions' :key='competition.id'>
                <b-nav-item>{{competition.name}}</b-nav-item>
            </div>
        </b-nav>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
    mounted() {
        this.$store.dispatch('getCompetitionsBySports')
    },
    computed: {
        ...mapGetters(['competitionsBySports'])
    }
}
</script>

<style scoped>
</style>
