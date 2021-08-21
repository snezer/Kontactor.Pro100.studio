import Vue from 'vue'
import Vuex from 'vuex'
import createLogger from 'vuex/dist/logger'

import editor from './modules/editor'
import user from './modules/user'
import chat from './modules/chat'
import company from './modules/company'

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  modules: {
    editor,
    user,
    chat,
    company
  },
  strict: debug,
  plugins: debug ? [createLogger()] : []
})
