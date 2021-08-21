<template>
  <div style="padding: 50px">
    <h2>Документы</h2>
    <v-data-table
        :headers="headers"
        :items="accruals"
    >
      <template v-slot:item.link="">
        <v-btn icon>
          <v-icon>
            mdi-cloud-download
          </v-icon>
        </v-btn>
      </template>
    </v-data-table>
  </div>
</template>

<script>
export default {
  name: "CRMFile",
  data(){
    return{
      headers: [
        {
          text: 'Название',
          align: 'start',
          sortable: false,
          value: 'name',
        },
        { text: 'Дата/Время', value: 'dateTime' },
        { text: 'Назначение', value: 'point' },
        { text: 'Статус', value: 'status' },
        { text: 'Скачать', value: 'link' },
      ],
      accruals: [
        {
          name: 'Договор',
          dateTime: '21.08.2021',
          point: 'Договор на аренду помещения',
          status: 'Оформлен',
          link: '/',
        },
      ],
    }
  }
}
</script>

<style scoped>

</style>