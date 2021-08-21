import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from "@/views/Home";
import HomeInfoLeft from "@/components/Home/HomeInfoLeft";
import HomeLogin from "@/components/Home/HomeLogin";
import HomeRegistration from "@/components/Home/HomeRegistration";
import Dashboard from "@/views/Dashboard";
import Editor from "@/views/Editor";
import Calendar from "@/views/Calendar";
import News from "@/views/News";
import DashboardMain from "@/components/Dashboard/DashboardMain";
import Messenger from "@/components/Dashboard/Messenger";
import CRMAccruals from "@/components/Dashboard/CRM/CRMAccruals";
import CRMPayment from "@/components/Dashboard/CRM/CRMPayment";
import CRMCounter from "@/components/Dashboard/CRM/CRMCounter";
import CRMFile from "@/components/Dashboard/CRM/CRMFile";
import CRMAppeal from "@/components/Dashboard/CRM/CRMAppeal";
import CRMContact from "@/components/Dashboard/CRM/CRMContact";
import CRMCall from "@/components/Dashboard/CRM/CRMCall";
import Workers from "@/components/Dashboard/Workers";
import Product from "@/components/Dashboard/Product";
import MapForUser from "@/components/MapForUser";
import RequestRent from "@/components/Dashboard/RequestRent";
import ClientCounters from '@/components/Dashboard/Counter/ClientCounters';

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
  {
    path: '/dashboard',
    name: 'dashboard',
    component: Dashboard,
    children: [
      {
        path: 'uk/request',
        name: 'requestRent',
        components: {
          default: Dashboard,
          dashboard: RequestRent
        }
      },
      {
        path: 'counters',
        name: 'clientCounters',
        components: {
          default: Dashboard,
          dashboard: ClientCounters
        }
      },
      {
        path: 'crm/',
        name: 'dashboardMain',
        components: {
          default: Dashboard,
          dashboard: DashboardMain
        },
        children:[
          {
            path: 'accruals',
            name: 'accruals',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMAccruals,
            }
          },
          {
            path: 'payment',
            name: 'payment',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMPayment,
            }
          },
          {
            path: 'counter',
            name: 'counter',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMCounter,
            }
          },
          {
            path: 'file',
            name: 'file',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMFile,
            }
          },
          {
            path: 'appeal',
            name: 'appeal',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMAppeal,
            }
          },
          {
            path: 'contact',
            name: 'contact',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMContact,
            }
          },
          {
            path: 'call',
            name: 'call',
            components:{
              default: Dashboard,
              dashboard: DashboardMain,
              dashboardMain: CRMCall,
            }
          },
        ]
      },
      {
        path: 'editor',
        name: 'editor',
        components: {
          default: Dashboard,
          dashboard: Editor
        }
      },
      {
        path: 'schedule',
        name: 'schedule',
        components: {
          default: Dashboard,
          dashboard: Calendar
        }
      },
      {
        path: 'news',
        name: 'news',
        components: {
          default: Dashboard,
          dashboard: News
        }
      },
      {
        path: 'messenger',
        name: 'messenger',
        components: {
          default: Dashboard,
          dashboard: Messenger
        }
      },
      {
        path: 'workers',
        name: 'workers',
        components: {
          default: Dashboard,
          dashboard: Workers
        }
      },
      {
        path: 'product',
        name: 'product',
        components: {
          default: Dashboard,
          dashboard: Product
        }
      },
    ]
  },
  {
    path: '/publicmap',
    name: 'publicmap',
    component: MapForUser
  }
]

const router = new VueRouter({
  mode: 'hash',
  base: process.env.BASE_URL,
  routes
})

export default router
