<template>
  <div class="request-wrapper">
    <div class="menu">

    </div>
    <div class="list-request-rent">
      <h2>Список заявок на аренду</h2>
      <v-data-table :items="rents" :headers="headers">
        <template v-slot:item.id="{ item}">
          <v-btn v-if="!item.isValidated" outlined @click="validationRent(item.id)" >
            <v-icon>
              mdi-check
            </v-icon> Подтвердить
          </v-btn>
          <span v-else>Заявка подтвержденна</span>
        </template>
      </v-data-table>
    </div>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";
import APICRMServices from "@/services/APICRMServices";

export default {
  name: "RequestRent",
  data(){
    return{
      headers: [
        {
          text: 'Наименование площадки',
          align: 'start',
          sortable: false,
          value: 'place',
        },
        {
          text: 'Заявитель',
          align: 'start',
          sortable: false,
          value: 'name',
        },
        {
          text: 'Срок аренды',
          align: 'start',
          sortable: false,
          value: 'time',
        },
        {
          text: 'Подтверждение',
          align: 'start',
          sortable: false,
          value: 'id',
        },
      ]
    }
  },
  computed:{
    ...mapGetters({rents: 'rents/allRentsFullData', userId: 'user/userId'})
  },
  methods:{
    ...mapActions({getRentsFullData: 'rents/loadRentsFullData'}),
    async validationRent(id){
        const result = await  APICRMServices.validationRent({ rentInfoId: id, userId: this.userId})
    }
  },
  created() {
    this.getRentsFullData()
  }
}
</script>

<style scoped lang="sass">
.request-wrapper
  width: 100%
  height: 100%
  display: grid
  grid-template-columns: 2fr 11fr
.menu
  position: relative
.menu::after
  position: absolute
  top: 5px
  right: 3px
  width: 1px
  height: 100%
  content: ''
  background-color: #eee
.list-request-rent
  padding: 50px
</style>