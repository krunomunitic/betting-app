<template>
    <div class="fixtures wallet">
        <b-row>
            <b-col sm="3">
                <label for="balance">Balance:</label>
            </b-col>
            <b-col sm="6">
                <b-form-input id="balance" type="number" :value="balance" @input="setBalance" min="0" placeholder="Balance"></b-form-input>
            </b-col>
        </b-row>
        <b-row>
            <b-col offset-sm="3" sm="6">
                <b-button squared @click="updateBalance()">
                    Set Balance
                </b-button>
            </b-col>
        </b-row>
    </div>
</template>

<script>
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
