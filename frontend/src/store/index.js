import Vue from 'vue'
import Vuex from 'vuex'
import createLogger from 'vuex/dist/logger'

import editor from './modules/editor'
import user from './modules/user'
import chat from './modules/chat'
import company from './modules/company'
import rents from "./modules/rents"

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  modules: {
    editor,
    user,
    chat,
    company,
    rents
  },
  strict: debug,
  plugins: debug ? [createLogger()] : []
})
