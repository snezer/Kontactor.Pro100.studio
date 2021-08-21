<template>
  <div class="workers-wrapper">
    <div class="workers-menu">

    </div>
    <div class="workers-list">
      <div class="control">
        <v-row>
          <v-col lg="7" class="title">
            Сотрудники  <span>(15 из 30)</span>
          </v-col>
          <v-col lg="1">
            <v-btn icon style="background-color: #eeeeee60">
              <v-icon>
                mdi-magnify
              </v-icon>
            </v-btn>
          </v-col>
          <v-col lg="2">
            <v-btn block rounded outlined color="primary">
              Выгрузить данные
            </v-btn>
          </v-col>
          <v-col lg="2">
            <v-btn block  elevation="0" rounded color="primary">
              Создать сотрудника
            </v-btn>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12">
            <v-tabs height="65">
              <v-tab>
                Работающие
              </v-tab>
              <v-tab>
                Уволенные
              </v-tab>
            </v-tabs>
          </v-col>
        </v-row>
      </div>
      <div class="list">
        <div class="filter">
          <v-menu offset-y class="elevation-0" v-for="i in 3" :key="i">
            <template v-slot:activator="{ on, attrs }">
              <div
                  v-bind="attrs"
                  v-on="on"
                  style="width: 150px"
              >
                Должность: <span class="blue--text font-weight-medium">Все</span> <v-icon>mdi-menu-down</v-icon>
              </div>
            </template>
            <v-list elevation="0">
              <v-list-item
                  v-for="(item, index) in items"
                  :key="index"
              >
                <v-list-item-title>{{ item.title }}</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </div>
        <div class="table-workers">
          <v-card flat>
            <v-card-title>
              Nutrition
              <v-spacer></v-spacer>
              <v-text-field
                  v-model="search"
                  append-icon="mdi-magnify"
                  label="Search"
                  single-line
                  hide-details
              ></v-text-field>
            </v-card-title>
            <v-data-table
                :headers="headers"
                :items="employees"
                :search="search"
            ></v-data-table>
          </v-card>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
export default {
  name: "Workers",
  computed: {
    ...mapGetters({
      companyId: 'user/companyId',
      employeesStore : 'company/employees'
    }),
    employees : function(){
      let cId = this.companyId;
      this.loadCompanyData(cId);
      return this.employeesStore;
    }    
  },
  methods:{
    ...mapActions({
      loadCompanyData : 'company/loadCompany'
    })
  },
  watch:{
    companyId : function(oldId, newId){
      if (newId)
        this.loadCompanyData(newId);
    }
  },
  
  data () {
    return {
      search: '',
      headers: [
        {
          text: 'Фамилия',
          align: 'start',
          sortable: false,
          value: 'lastName',
        },
        { text: 'Имя', value: 'firstName' },
        { text: 'Отчество', value: 'middleName' },
        { text: 'Должность', value: 'position' },
        { text: 'Some', value: 'protein' },
        { text: 'Iron (%)', value: 'iron' },
      ],
      desserts: [
        {
          name: 'Frozen Yogurt',
          calories: 159,
          fat: 6.0,
          carbs: 24,
          protein: 4.0,
          iron: '1%',
        },
        {
          name: 'Ice cream sandwich',
          calories: 237,
          fat: 9.0,
          carbs: 37,
          protein: 4.3,
          iron: '1%',
        },
        {
          name: 'Eclair',
          calories: 262,
          fat: 16.0,
          carbs: 23,
          protein: 6.0,
          iron: '7%',
        },
        {
          name: 'Cupcake',
          calories: 305,
          fat: 3.7,
          carbs: 67,
          protein: 4.3,
          iron: '8%',
        },
        {
          name: 'Gingerbread',
          calories: 356,
          fat: 16.0,
          carbs: 49,
          protein: 3.9,
          iron: '16%',
        },
        {
          name: 'Jelly bean',
          calories: 375,
          fat: 0.0,
          carbs: 94,
          protein: 0.0,
          iron: '0%',
        },
        {
          name: 'Lollipop',
          calories: 392,
          fat: 0.2,
          carbs: 98,
          protein: 0,
          iron: '2%',
        },
        {
          name: 'Honeycomb',
          calories: 408,
          fat: 3.2,
          carbs: 87,
          protein: 6.5,
          iron: '45%',
        },
        {
          name: 'Donut',
          calories: 452,
          fat: 25.0,
          carbs: 51,
          protein: 4.9,
          iron: '22%',
        },
        {
          name: 'KitKat',
          calories: 518,
          fat: 26.0,
          carbs: 65,
          protein: 7,
          iron: '6%',
        },
      ],
    }
  },
}
</script>

<style scoped lang="sass">
.workers-wrapper
  width: 100%
  height: 100%
  display: grid
  grid-template-columns: 2fr 10fr
.workers-menu
  position: relative
  background-color: #f5f8fa
.workers-menu::after
  position: absolute
  top: 1%
  right: 0
  width: 1px
  height: 99%
  content: ''
  background-color: #eee
.workers-list
  display: grid
  grid-template-rows: 150px 1fr
.control
  position: relative
  flex-wrap: wrap
  padding: 25px 2%
.control::after
  position: absolute
  bottom: 0
  width: 96%
  height: 1px
  background-color: #eee
  content: ''
.control .title
  font-size: 2em
  font-weight: bold
.list
  display: grid
  grid-template-rows: 50px 1fr
  margin-left: 2%
.filter
  display: flex
  justify-content: start
  align-items: center
</style>