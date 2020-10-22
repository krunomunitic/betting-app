import Vue from "vue";
import Router from "vue-router";

import Fixtures from '../components/Fixtures';
import Ticket from '../components/Ticket';
import Wallet from '../components/Wallet';

Vue.use(Router);

export default new Router({
    mode: "history",
    routes: [
        {
            path: "/",
            name: "fixtures",
            component: Fixtures
        },
        {
            path: "/ticket",
            name: "ticket",
            component: Ticket
        },
        {
            path: "/wallet",
            name: "wallet",
            component: Wallet
        },
        { path: '*', redirect: { name: 'fixtures' } }
    ]
});