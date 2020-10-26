<template>
    <div>
        <sidebar />
        <b-row align-h="between" class="fixtures fixtures__special">
            <b-col cols="12">
                <h2>Special Offers</h2>
            </b-col>

            <b-col cols="12">
                <div class="fixtures__competition"
                     v-for="fixtureCompetition in fixturesByCompetitionSpecial"
                     :key="fixtureCompetition.competitionName + `Special`"
                 >
                    <b-row>
                        <b-col cols="5">
                            <h4>{{fixtureCompetition.competitionName}}</h4>
                        </b-col>
                        <b-col cols="6" class="fixtures__competition__fixture__odds">
                            <b-row>
                                <div v-for="oddsType in oddsTypes" :key="oddsType">
                                    <b-col cols="2">
                                        <div class="fixtures__competition__fixture__odds__name">{{oddsType}}</div>
                                    </b-col>
                                </div>
                            </b-row>
                        </b-col>
                    </b-row>
                    <div v-for="fixture in fixtureCompetition.fixtures" :key="fixture.id">
                        <b-row align-h="between" class="fixtures__competition__fixture">
                            <b-col cols="5" class="fixtures__competition__fixture__info">
                                {{fixture.date | moment}} {{fixture.homeTeamName}} - {{fixture.awayTeamName}} {{fixture.result}}
                            </b-col>
                            <b-col cols="7" class="fixtures__competition__fixture__odds">
                                <b-row>
                                    <div v-for="oddsType in oddsTypes" :key="oddsType" class="fixtures__competition__fixture__odds__single">
                                        <b-col cols="1">
                                            <b-button v-if="fixture.odds[oddsType] && fixture.odds[oddsType].value"
                                                      variant="outline-primary"
                                                      @click="addBet(fixture, fixture.odds[oddsType], oddsType, true);
                                                              updateFixturesWithStatus(fixtureCompetition.competitionName, fixture.id, oddsType, true);"
                                                      :pressed="fixture.odds[oddsType] && fixture.odds[oddsType].betted && fixture.hasBet">
                                                {{fixture.odds[oddsType] && fixture.odds[oddsType].value.toFixed(2)}}
                                            </b-button>
                                        </b-col>
                                    </div>
                                    <b-col cols="1">
                                        <b-button class="fixtures__competition__fixture__odds__remove"
                                                  v-if="fixture.hasBet"
                                                  variant="outline-danger"
                                                  @click="removeBet(fixture.id);
                                                          updateFixturesWithStatus(fixtureCompetition.competitionName, fixture.id, null, true);">
                                            x
                                        </b-button>
                                    </b-col>
                                </b-row>
                            </b-col>

                        </b-row>
                    </div>
                </div>
            </b-col>
        </b-row>

        <b-row align-h="between" class="fixtures">
            <b-col cols="12">
                <h2>Offers</h2>
            </b-col>

            <b-col cols="12">
                <div class="fixtures__competition"
                     v-for="fixtureCompetition in fixturesByCompetition"
                     :key="fixtureCompetition.competitionName"
                >
                    <b-row>
                        <b-col cols="5">
                            <h4>{{fixtureCompetition.competitionName}}</h4>
                        </b-col>
                        <b-col cols="6" class="fixtures__competition__fixture__odds">
                            <b-row>
                                <div v-for="oddsType in oddsTypes" :key="oddsType">
                                    <b-col cols="2">
                                        <div class="fixtures__competition__fixture__odds__name">{{oddsType}}</div>
                                    </b-col>
                                </div>
                            </b-row>
                        </b-col>
                    </b-row>
                    <div v-for="fixture in fixtureCompetition.fixtures" :key="fixture.id">
                        <b-row align-h="between" class="fixtures__competition__fixture">
                            <b-col cols="5" class="fixtures__competition__fixture__info">
                                {{fixture.date | moment}} {{fixture.homeTeamName}} - {{fixture.awayTeamName}} {{fixture.result}}
                            </b-col>
                            <b-col cols="7" class="fixtures__competition__fixture__odds">
                                <b-row>
                                    <div v-for="oddsType in oddsTypes" :key="oddsType" class="fixtures__competition__fixture__odds__single">
                                        <b-col cols="1">
                                            <b-button v-if="fixture.odds[oddsType] && fixture.odds[oddsType].value"
                                                      variant="outline-primary"
                                                      @click="addBet(fixture, fixture.odds[oddsType], oddsType);
                                                              updateFixturesWithStatus(fixtureCompetition.competitionName, fixture.id, oddsType);"
                                                      :pressed="fixture.odds[oddsType] && fixture.odds[oddsType].betted && fixture.hasBet">
                                                {{fixture.odds[oddsType] && fixture.odds[oddsType].value.toFixed(2)}}
                                            </b-button>
                                        </b-col>
                                    </div>
                                    <b-col cols="1">
                                        <b-button class="fixtures__competition__fixture__odds__remove"
                                                  v-if="fixture.hasBet"
                                                  variant="outline-danger"
                                                  @click="removeBet(fixture.id);
                                                          updateFixturesWithStatus(fixtureCompetition.competitionName, fixture.id);">
                                            x
                                        </b-button>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                    </div>
                </div>
            </b-col>
        </b-row>

        <b-button squared v-if="ticket && ticket.bets && ticket.bets.length"
                  @click="validateTicket()">
            Place Bet
        </b-button>
    </div>
</template>

<script>
    import moment from 'moment'
    import { mapGetters } from 'vuex'
    import Sidebar from './Sidebar.vue'
    import { oddsTypes } from '../config/oddsTypes'

    export default {
        components: {
            Sidebar
        },
        data() {
            return {
                oddsTypes
            };
        },
        mounted() {
            this.$store.dispatch('getFixtures')
        },
        computed: {
            ...mapGetters(['fixturesByCompetition', 'fixturesByCompetitionSpecial', 'ticket'])
        },
        methods: {
            addBet({ id, date, homeTeamName, awayTeamName }, odds, oddsType, special = false) {
                this.$store.dispatch('addBet',
                    { fixtureId: id, date, homeTeamName, awayTeamName, odds, oddsType, special })
            },
            updateFixturesWithStatus(competitionName, fixtureId, oddsType = null, special = false) {
                this.$store.dispatch('updateFixturesWithStatus', { competitionName, fixtureId, oddsType, special })
            },
            removeBet(fixtureId) {
                this.$store.dispatch('removeBet', fixtureId)
            },
            betOnTicket() {
                this.$store.dispatch('betOnTicket')
            },
            validateTicket() {
                this.$store.dispatch('validateTicket')
            },
        },
        filters: {
            moment(date) {
                return moment(date).format('HH:mm');
            },
        }
    }</script>

<style scoped>
</style>
