import Vue from "vue";
import Router from "vue-router";

import fixtures from '../components/Fixtures';
// import wallet from './components/Wallet';

Vue.use(Router);

export default new Router({
    mode: "history",
    routes: [
        {
            path: "/",
            alias: "/",
            name: "fixtures",
            component: fixtures
        },
        /*{
            path: "/wallet",
            name: "wallet",
            component: wallet
        },*/
        { path: '*', redirect: { name: 'fixtures' } }
    ]
});