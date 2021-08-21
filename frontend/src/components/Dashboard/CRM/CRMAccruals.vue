<template>
  <div class="accruals-wrapper">
    <h2>Начисления</h2>
    <v-data-table
        :headers="headers"
        :items="accruals"
    >
      <template v-slot:item.status="{ item}">
        {{item.status}}
        <v-btn icon v-if="item.status=='Ожидает оплаты'">
          <v-icon>
            mdi-credit-card-outline
          </v-icon>
        </v-btn>
      </template>
    </v-data-table>
  </div>
</template>

<script>
export default {
  name: "CRMAccruals",
  data () {
    return {
      search: '',
      headers: [
        {
          text: 'Название',
          align: 'start',
          sortable: false,
          value: 'name',
        },
        { text: 'Дата/Время', value: 'dateTime' },
        { text: 'Назначение', value: 'point' },
        { text: 'Сумма', value: 'coins' },
        { text: 'Статус', value: 'status' },
      ],
      accruals: [
        {
          name: 'Оплата договора',
          dateTime: '21.08.2021',
          point: 'Оплата договора аренды помещения',
          coins: '100 000',
          status: 'Ожидает оплаты',
        },
      ],
    }
  },
}
</script>

<style scoped lang="sass">
.accruals-wrapper
  padding: 50px
</style>