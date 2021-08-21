<template>
  <div style="padding: 50px">
    <h2>Документы</h2>
    <v-data-table :items="rents" :headers="headers">
      <template v-slot:item.id="{ item}">
        <v-btn icon v-if="item.isValidated" outlined link :href="'http://01fe1d0d97fb.ngrok.io/api/v1/Rents/contract/blob/'+item.id" target="_blank">
          <v-icon>
            mdi-download-outline
          </v-icon>
        </v-btn>
        <span v-else>Заявка не подтвержденна</span>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {
  name: "CRMFile",
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
  },
  created() {
    this.getRentsFullData()
  }
}
</script>

<style scoped>

</style>