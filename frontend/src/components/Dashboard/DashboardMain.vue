<template>
  <div class="dashboard-main-wrapper">
    <div class="dashboard-main-menu">
      <v-list >
        <v-list-item v-for="item in itemsMenu" link :to="item.link" :key="item.title">
          <v-list-item-avatar>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-avatar>
          <v-list-item-content>
            <v-list-item-title>
              {{item.title}}
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </div>
    <div class="dashboard-main-content">
      <router-view name="dashboardMain"></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: "DashboardMain",
  data(){
    return{
      itemsMenu: [
        {
          title: 'Начисления',
          link: '/dashboard/crm/accruals',
          icon: 'mdi-account-cash-outline'
        },
        {
          title: 'История оплат',
          link: '/dashboard/crm/payment',
          icon: 'mdi-account-cash-outline'
        },
        {
          title: 'Счетчики',
          link: '/dashboard/crm/counter',
          icon: 'mdi-account-cash-outline'
        },
        {
          title: 'Документы',
          link: '/dashboard/crm/file',
          icon: 'mdi-account-cash-outline'
        },
        {
          title: 'Обращения',
          link: '/dashboard/crm/appeal',
          icon: 'mdi-account-cash-outline'
        },
        {
          title: 'Контакты',
          link: '/dashboard/crm/contact',
          icon: 'mdi-account-cash-outline'
        },
        {
          title: 'Позвонить',
          link: '/dashboard/crm/call',
          icon: 'mdi-account-cash-outline'
        },
      ]
    }
  }
}
</script>

<style scoped lang="sass">
@import "src/sass/color"
.dashboard-main-wrapper
  width: 100%
  height: 100%
  display: grid
  grid-template-columns: 2fr 11fr
.dashboard-main-menu
  position: relative
.dashboard-main-menu::after
  position: absolute
  top: 5px
  right: 3px
  width: 1px
  height: 100%
  content: ''
  background-color: #eee
.activeItem::after
  position: absolute
  top: 0
  right: 0
  width: 2px
  height: 100%
  background-color: $blue-dark
  content: ''
.v-list .v-list-item--active
  color: #1976d2
</style>