<template>
  <div class="counter-wrapper">
    <h2>Текущие показания</h2>
    <v-data-table
        :headers="headers"
        :items="counter"
        hide-default-footer
    >
      <template v-slot:item.value="{ item }">
        <v-text-field style="width: 300px" :value="item.value"></v-text-field>
      </template>
    </v-data-table>
    <v-row>
      <v-col class="text-right">
        <v-btn outlined color="primary">
          Подать показания
        </v-btn>
      </v-col>
    </v-row>
    <h2 style="margin-top: 50px">Статистика</h2>
    <div class="stat">
      <v-card
          class="mt-4 mx-auto"
          max-width="400"
      >
        <v-sheet
            class="v-sheet--offset mx-auto"
            color="blue"
            elevation="12"
            max-width="calc(100% - 32px)"
        >
          <v-sparkline
              :labels="labels"
              :value="value"
              color="white"
              line-width="2"
              padding="16"
              label-size="12"
          ></v-sparkline>
        </v-sheet>

        <v-card-text class="pt-0">
          <div class="text-h6 font-weight-light mb-2">
            Потребление ХВС
          </div>
          <v-divider class="my-2"></v-divider>
          <v-icon
              class="mr-2"
              small
          >
            mdi-clock
          </v-icon>
          <span class="text-caption grey--text font-weight-light">Данные обновляются 30 числа текущего месяца</span>
        </v-card-text>
      </v-card>
      <v-card
          class="mt-4 mx-auto"
          max-width="400"
      >
        <v-sheet
            class="v-sheet--offset mx-auto"
            color="pink"
            elevation="12"
            max-width="calc(100% - 32px)"
        >
          <v-sparkline
              :labels="labels"
              :value="value"
              color="white"
              line-width="2"
              padding="16"
              label-size="12"
          ></v-sparkline>
        </v-sheet>

        <v-card-text class="pt-0">
          <div class="text-h6 font-weight-light mb-2">
            Потребление ГВС
          </div>
          <v-divider class="my-2"></v-divider>
          <v-icon
              class="mr-2"
              small
          >
            mdi-clock
          </v-icon>
          <span class="text-caption grey--text font-weight-light">Данные обновляются 30 числа текущего месяца</span>
        </v-card-text>
      </v-card>
      <v-card
          class="mt-4 mx-auto"
          max-width="400"
      >
        <v-sheet
            class="v-sheet--offset mx-auto"
            color="cyan"
            elevation="12"
            max-width="calc(100% - 32px)"
        >
          <v-sparkline
              :labels="labels"
              :value="value"
              color="white"
              line-width="2"
              padding="16"

              label-size="12"
          ></v-sparkline>
        </v-sheet>

        <v-card-text class="pt-0">
          <div class="text-h6 font-weight-light mb-2">
            Потребление Эль. Энергии
          </div>
          <v-divider class="my-2"></v-divider>
          <v-icon
              class="mr-2"
              small
          >
            mdi-clock
          </v-icon>
          <span class="text-caption grey--text font-weight-light">Данные обновляются 30 числа текущего месяца</span>
        </v-card-text>
      </v-card>
    </div>
  </div>
</template>

<script>
export default {
  name: "CRMCounter",
  data() {
    return {
      headers: [
        {
          text: 'Название',
          align: 'start',
          sortable: false,
          value: 'name',
        },
        {text: 'Дата/Время', value: 'dateTime'},
        {text: 'Показания', value: 'value'},
        {text: 'Сумма', value: 'coins'},
      ],
      counter: [
        {
          name: 'ГВС',
          dateTime: '21.08.2021',
          value: '0',
          coins: '0',
        },
        {
          name: 'ХВС',
          dateTime: '21.08.2021',
          value: '0',
          coins: '0',
        },
        {
          name: 'Эл. энергия',
          dateTime: '21.08.2021',
          value: '0',
          coins: '0',
        },
      ],
      labels: [
        'Дек',
        'Янв',
        'Фев',
        'Мар',
        'Апр',
        'Май',
        'Июн',
        'Июл',
      ],
      value: [
        200,
        675,
        410,
        390,
        310,
        460,
        250,
        240,
      ],
    }
  }
}
</script>

<style scoped lang="sass">
.counter-wrapper
  padding: 50px
.v-sheet--offset
  top: -24px
  position: relative
.stat
  display: flex
  justify-content: center
  align-items: center
  height: 350px
</style>