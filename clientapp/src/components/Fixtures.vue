<template>
    <div>
        <div class="fixtures" v-for="fixturesGroup in fixturesGrouped" :key="fixturesGroup.name">
            <b-row>
                <b-col cols="12">
                    <h2>{{fixturesGroup.name}}</h2>
                </b-col>

                <b-col cols="12">
                    <div class="fixtures__sport" v-for="sport in sports" :key="sport.name">
                        <b-row v-if="fixturesGroup.fixtures[sport.name]">
                            <b-col cols="12">
                                <h4>{{sport.name}}</h4>
                            </b-col>

                            <b-col cols="12">   
                                <div class="fixtures__competition" v-for="competition in sport.competitions" :key="competition">
                                    <div cols="12" v-if="fixturesGroup.fixtures[sport.name] && fixturesGroup.fixtures[sport.name][competition]">
                                        <b-row>
                                            <b-col cols="5">
                                                <h5>{{competition}}</h5>
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


                                        <div v-for="fixture in fixturesGroup.fixtures[sport.name][competition]" :key="fixture.id">
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
                                                                          @click="addBet(fixture, fixture.odds[oddsType], oddsType, fixturesGroup.name);
                                                                                  updateFixturesWithStatus(fixture.id, fixturesGroup.name, oddsType);"
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
                                                                              updateFixturesWithStatus(fixture.id, fixturesGroup.name, null);">
                                                                x
                                                            </b-button>
                                                        </b-col>
                                                    </b-row>
                                                </b-col>
                                            </b-row>
                                        </div>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </div>
                </b-col>
            </b-row>
        </div>
        <b-button class="fixtures__place-bet"
                  v-if="ticket && ticket.bets && ticket.bets.length"
                  @click="validateTicket()">
            Place Bet
        </b-button>
    </div>
</template>

<script>import _ from 'lodash'
    import moment from 'moment'
    import { oddsTypes } from '../config/oddsTypes'
    import { sports } from '../config/sportsCompetitions'

    export default {
        data() {
            return {
                oddsTypes,
                sports
            };
        },
        mounted() {
            this.$store.dispatch('getFixtures')
        },
        computed: {
            fixturesGrouped() {
                const fixtures = this.$store.getters.fixtures

                const fixturesFormatted = []
                fixtures.forEach(f => {
                    const tempFixturesFormatted = _.groupBy(f.fixtures, item => {
                        return item.sportName;
                    });

                    _.forEach(tempFixturesFormatted, (value, key) => {
                        tempFixturesFormatted[key] =
                            _.groupBy(tempFixturesFormatted[key], item => {
                                return item.competitionName;
                            });
                    });

                    fixturesFormatted.push({ name: f.name, fixtures: tempFixturesFormatted })
                })

                return fixturesFormatted
            },
            ticket() {
                return this.$store.getters.ticket
            }
        },
        methods: {
            addBet({ id, date, homeTeamName, awayTeamName }, odds, oddsType, fixturesGroupName) {
                this.$store.dispatch('addBet',
                    { fixtureId: id, date, homeTeamName, awayTeamName, odds, oddsType, fixturesGroupName })
            },
            updateFixturesWithStatus(fixtureId, fixturesGroupName, oddsType = null) {
                this.$store.dispatch('updateFixturesWithStatus', { fixtureId, fixturesGroupName, oddsType })
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
