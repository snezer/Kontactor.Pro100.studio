<template>
  <div class="dashboard">
    <div class="navigation-bar">
      <div class="rent-place">
        <div class="logo">
          <v-img :src="require('@/assets/miniLogo.svg')" max-width="50px" height="auto"></v-img>
        </div>
        <v-menu
            top
        >
          <template v-slot:activator="{ on, attrs }">
            <v-list v-if="!userUKPersonnel">
              <v-list-item
                  v-bind="attrs"
                           v-on="on">
                <v-list-item-avatar>
                  <img
                      src="https://cdn.vuetifyjs.com/images/john.jpg"
                      alt="John"
                  >
                </v-list-item-avatar>

                <v-list-item-content>
                    <v-list-item-subtitle>IT Hub</v-list-item-subtitle>
                    <v-list-item-title>Программируем и печем</v-list-item-title>
                </v-list-item-content>
                <v-list-item-action>
                  <v-icon>
                    mdi-chevron-down
                  </v-icon>
                </v-list-item-action>
              </v-list-item>
            </v-list>
          </template>

          <v-list>
            <v-list-item
                v-for="(item, index) in itemUserMenu"
                :key="index"
            >
              <v-list-item-avatar>
                <img
                    src="https://cdn.vuetifyjs.com/images/john.jpg"
                    alt="John"
                >
              </v-list-item-avatar>

              <v-list-item-content>
                <v-list-item-subtitle>Name rent place {{index}}</v-list-item-subtitle>
                <v-list-item-title>Custom name rent place {{index}}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-menu>
      </div>
      <div class="navigation">
        <v-tabs height="80">
          <v-tab
                v-for="item in navigationMenuItems"
                link
                style="min-width: 120px"
                :to="item.link"
                :key="item.link"
          >
            {{item.title}}
          </v-tab>
        </v-tabs>
      </div>
      <div class="user-info">
        <v-btn icon>
          <v-badge content="5" color="red" overlap>
            <v-icon >
              mdi-bell-outline
            </v-icon>
          </v-badge>
        </v-btn>
        <v-menu
            top
        >
          <template v-slot:activator="{ on, attrs }">
            <v-list >
              <v-list-item v-bind="attrs"
                           v-on="on">
                <v-list-item-avatar>
                  <img
                      src="https://cdn.vuetifyjs.com/images/john.jpg"
                      alt="John"
                  >
                </v-list-item-avatar>

                <v-list-item-content>
                  <v-list-item-title>{{userFIO}}</v-list-item-title>
                </v-list-item-content>
                <v-list-item-action>
                  <v-icon>
                    mdi-chevron-down
                  </v-icon>
                </v-list-item-action>
              </v-list-item>
            </v-list>
          </template>

          <v-list>
            <v-list-item
                v-for="(item, index) in itemUserMenu"
                :key="index"
            >
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
      </div>
    </div>
    <v-main>
      <router-view name="dashboard"></router-view>
    </v-main>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: "Dashboard",
  data(){
    return{
      itemUserMenu: [
        { title: 'Click Me' },
        { title: 'Click Me' },
        { title: 'Click Me' },
        { title: 'Click Me 2' },
      ],
      items: [
        { title: 'Главная', icon: 'mdi-home-city', link:'/dashboard/crm/accruals' },
        { title: 'Карта', icon: 'mdi-map', link: '/dashboard/editor' },
        { title: 'Показания', link: '/dashboard/counters'},
        { title: 'Расписание', icon: 'mdi-calendar', link: '/dashboard/schedule' },
        { title: 'Лента', icon: 'mdi-newspaper-variant-outline', link: '/dashboard/news' },
        { title: 'Сотрудники', icon: 'mdi-account', link: '/dashboard/workers' },
        { title: 'Товары/Услуги', icon: 'mdi-account', link: '/dashboard/product' },
        { title: 'Чат', icon: 'mdi-chat', link: '/dashboard/messenger' },
      ],
      itemsMenuForUK: [
        { title: 'Заявки на аренду', link: '/dashboard/uk/request'},
        { title: 'Показания', link: '/dashboard/uk/counter'},
        { title: 'Карта', icon: 'mdi-map', link: '/dashboard/editor' },
        { title: 'Статистика', link: '/dashboard/uk/stat'},
        { title: 'Сотрудники', icon: 'mdi-account', link: '/dashboard/workers' },
        { title: 'Расписание', icon: 'mdi-calendar', link: '/dashboard/schedule' },
        { title: 'Чат', icon: 'mdi-chat', link: '/dashboard/messenger' },
      ]
    }
  },
  computed: {
    ...mapGetters({isAuthenticated:'user/isAuthenticated', userId : 'user/userId', userFIO:'user/userFIO', userUKPersonnel: "user/isUKPersonnel"}),
    navigationMenuItems(){
      return this.userUKPersonnel ? this.itemsMenuForUK : this.items ;
    }
  }
}
</script>

<style lang="sass">
@import "src/sass/color"
.dashboard
  width: 100%
  height: 100%
  display: grid
  grid-template-rows: 80px 1fr
.navigation-bar
  display: grid
  grid-template-columns: 3fr 7fr 3fr
  box-shadow: 0 0 15px #eee
  z-index: 1000
.logo
  background-color: $blue-dark
  display: flex
  justify-content: center
  align-items: center
  border-radius: 0 0 10px 0
.rent-place
  position: relative
  display: grid
  grid-template-columns: 75px 1fr

.rent-place::after
  position: absolute
  top: 8px
  right: 3px
  content: ' '
  width: 1px
  height: 64px
  background: #eee
.navigation
  padding-left: 25px
.user-info
  width: 100%
  padding-right: 50px
  display: flex
  justify-content: flex-end
  align-items: center
  gap: 10px
</style>