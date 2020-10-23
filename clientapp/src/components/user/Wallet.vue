<template>
    <div>
        <b-row>
            <b-col sm="6">
                <b-form-input type="number" :value="balance" @input="setBalance" min="0" placeholder="Balance" ></b-form-input>
            </b-col>
            <b-col sm="5">
                <b-button squared @click="updateBalance()">
                    Set Balance
                </b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    // import { mapActions } from 'vuex'
    import { mapGetters } from 'vuex'

    export default {
        data() {
            return {
                tempBalance: 0
            }
        },
        mounted() {
            this.$store.dispatch('getWalletBalance')
        },
        computed: {
            ...mapGetters(['balance'])
        },
        methods: {
            // ...mapActions(['addBet', './store/index']),
            setBalance(value) {
                this.tempBalance = value
            },
            updateBalance() {
                if (Number(this.tempBalance) < 0) {
                    console.log("error")
                    return;
                }

                this.$store.dispatch('updateBalance', Number(this.tempBalance))
            }
        },
    }
</script>

<style scoped>
</style>
