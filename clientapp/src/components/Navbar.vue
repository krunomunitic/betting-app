<template>
    <div>
        <b-navbar toggleable="lg" type="dark" variant="info">
            <b-navbar-nav class="ml-auto">
            <b-nav-item href="#">Wallet</b-nav-item>
        </b-navbar-nav>
        </b-navbar>
    </div>
</template>

<script>
export default {
    name: 'Navbar',
    props: {
        msg: String
    }
}
</script>

<style scoped>
</style>
