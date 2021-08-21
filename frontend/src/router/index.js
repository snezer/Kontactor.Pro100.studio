import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import HomeInfoLeft from "@/components/Home/HomeInfoLeft";
import HomeLogin from "@/components/Home/HomeLogin";
import HomeRegistration from "@/components/Home/HomeRegistration";

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    component: Home,
    children:[
      {
        path: '',
        name: 'homeInfo',
        components: {
          default: Home,
          homeLeftBlock: HomeInfoLeft
        },
      },
      {
        path: 'login',
        name: 'login',
        components:{
          default: Home,
          homeLeftBlock: HomeLogin
        }
      },
    ]
  },
  {
    path: '/registration',
    name: 'registration',
    component: HomeRegistration

  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router