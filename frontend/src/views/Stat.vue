<template>
  <v-container
      id="dashboard"
      fluid
      tag="section"
  >
    <v-row>
      <v-col
          cols="12"
          lg="4"
      >
        <base-material-chart-card
            :data="emailsSubscriptionChart.data"
            :options="emailsSubscriptionChart.options"
            :responsive-options="emailsSubscriptionChart.responsiveOptions"
            color="#E91E63"
            hover-reveal
            type="Bar"
        >
          <template v-slot:reveal-actions>
            <v-tooltip bottom>
              <template v-slot:activator="{ attrs, on }">
                <v-btn
                    v-bind="attrs"
                    color="info"
                    icon
                    v-on="on"
                >
                  <v-icon
                      color="info"
                  >
                    mdi-refresh
                  </v-icon>
                </v-btn>
              </template>

              <span>Refresh</span>
            </v-tooltip>

            <v-tooltip bottom>
              <template v-slot:activator="{ attrs, on }">
                <v-btn
                    v-bind="attrs"
                    light
                    icon
                    v-on="on"
                >
                  <v-icon>mdi-pencil</v-icon>
                </v-btn>
              </template>

              <span>Change Date</span>
            </v-tooltip>
          </template>

          <h4 class="card-title font-weight-light mt-2 ml-2">
            Посетители Пульсар.ru
          </h4>

          <p class="d-inline-flex font-weight-light ml-2 mt-1 red--text">
            Среднее количество отказов 7.5%
          </p>

          <template v-slot:actions>
            <v-icon
                class="mr-1"
                small
            >
              mdi-clock-outline
            </v-icon>
            <span class="caption grey--text font-weight-light">Обновлено 10 минут назад</span>
          </template>
        </base-material-chart-card>
      </v-col>

      <v-col
          cols="12"
          lg="4"
      >
        <base-material-chart-card
            :data="dailySalesChart.data"
            :options="dailySalesChart.options"
            color="success"
            hover-reveal
            type="Line"
        >
          <template v-slot:reveal-actions>
            <v-tooltip bottom>
              <template v-slot:activator="{ attrs, on }">
                <v-btn
                    v-bind="attrs"
                    color="info"
                    icon
                    v-on="on"
                >
                  <v-icon
                      color="info"
                  >
                    mdi-refresh
                  </v-icon>
                </v-btn>
              </template>

              <span>Refresh</span>
            </v-tooltip>

            <v-tooltip bottom>
              <template v-slot:activator="{ attrs, on }">
                <v-btn
                    v-bind="attrs"
                    light
                    icon
                    v-on="on"
                >
                  <v-icon>mdi-pencil</v-icon>
                </v-btn>
              </template>

              <span>Change Date</span>
            </v-tooltip>
          </template>

          <h4 class="card-title font-weight-light mt-2 ml-2">
            Пользователи Пульсар.ru
          </h4>

          <p class="d-inline-flex font-weight-light ml-2 mt-1">
            <v-icon
                color="green"
                small
            >
              mdi-arrow-up
            </v-icon>
            <span class="green--text">55%</span>&nbsp;
            рост за неделю
          </p>

          <template v-slot:actions>
            <v-icon
                class="mr-1"
                small
            >
              mdi-clock-outline
            </v-icon>
            <span class="caption grey--text font-weight-light">обновлено 10 минут назад</span>
          </template>
        </base-material-chart-card>
      </v-col>

      <v-col
          cols="12"
          lg="4"
      >
        <base-material-chart-card
            :data="dataCompletedTasksChart.data"
            :options="dataCompletedTasksChart.options"
            hover-reveal
            color="info"
            type="Line"
        >
          <template v-slot:reveal-actions>
            <v-tooltip bottom>
              <template v-slot:activator="{ attrs, on }">
                <v-btn
                    v-bind="attrs"
                    color="info"
                    icon
                    v-on="on"
                >
                  <v-icon
                      color="info"
                  >
                    mdi-refresh
                  </v-icon>
                </v-btn>
              </template>

              <span>Refresh</span>
            </v-tooltip>

            <v-tooltip bottom>
              <template v-slot:activator="{ attrs, on }">
                <v-btn
                    v-bind="attrs"
                    light
                    icon
                    v-on="on"
                >
                  <v-icon>mdi-pencil</v-icon>
                </v-btn>
              </template>

              <span>Change Date</span>
            </v-tooltip>
          </template>

          <h3 class="card-title font-weight-light mt-2 ml-2">
            Сформированно путевых листов
          </h3>

          <p class="d-inline-flex font-weight-light ml-2 mt-1">
            Ошибок при формировании не выявлено
          </p>

          <template v-slot:actions>
            <v-icon
                class="mr-1"
                small
            >
              mdi-clock-outline
            </v-icon>
            <span class="caption grey--text font-weight-light">обновлено 5 минут назад</span>
          </template>
        </base-material-chart-card>
      </v-col>

      <v-col
          cols="12"
          sm="6"
          lg="3"
      >
        <base-material-stats-card
            color="info"
            icon="mdi-card-account-details"
            title="Пользователей онлайн"
            value="14"
            sub-icon="mdi-clock"
            sub-text="Обновлено сейчас"
        />
      </v-col>

      <v-col
          cols="12"
          sm="6"
          lg="3"
      >
        <base-material-stats-card
            color="primary"
            icon="mdi-poll"
            title="Посетителей в час"
            value="15.521"
            sub-icon="mdi-tag"
            sub-text="Данные Яндекс.Метрика"
        />
      </v-col>

      <v-col
          cols="12"
          sm="6"
          lg="3"
      >
        <base-material-stats-card
            color="success"
            icon="mdi-transit-detour"
            title="Активных рейсов"
            value="1872"
            sub-icon="mdi-calendar"
            sub-text="За последние 24 часа"
        />
      </v-col>

      <v-col
          cols="12"
          sm="6"
          lg="3"
      >
        <base-material-stats-card
            color="orange"
            icon="mdi-transit-detour"
            title="Завершенных рейсов"
            value="184"
            sub-icon="mdi-calendar"
            sub-icon-color="grey darken-1"
            sub-text="За последние 24 часа"
        />
      </v-col>

      <v-col
          cols="12"
          md="6"
      >
        <base-material-card
            color="warning"
            class="px-5 py-3"
        >
          <template v-slot:heading>
            <div class="display-2 font-weight-light">
              Действующие пассажироперевозчики
            </div>

            <div class="subtitle-1 font-weight-light">
              Обновлено 26 минут назад
            </div>
          </template>
          <v-card-text>
            <v-data-table
                locales="ru"
                :headers="headers"
                :items="items"
            />
          </v-card-text>
        </base-material-card>
      </v-col>

      <v-col
          cols="12"
          md="6"
      >
        <base-material-card class="px-5 py-3">
          <template v-slot:heading>
            <v-tabs
                v-model="tabs"
                background-color="transparent"
                slider-color="white"
            >
              <span
                  class="subheading font-weight-light mx-3"
                  style="align-self: center"
              >Задачи: </span>
              <v-tab class="mr-3">
                <v-icon class="mr-2">
                  mdi-bug
                </v-icon>
                Ошибки
              </v-tab>
              <v-tab class="mr-3">
                <v-icon class="mr-2">
                  mdi-code-tags
                </v-icon>
                Клиенты
              </v-tab>
              <v-tab>
                <v-icon class="mr-2">
                  mdi-cloud
                </v-icon>
                Сервер
              </v-tab>
            </v-tabs>
          </template>

          <v-tabs-items
              v-model="tabs"
              class="transparent"
          >
            <v-tab-item
                v-for="n in 3"
                :key="n"
            >
              <v-card-text>
                <template v-for="(task, i) in tasks[tabs]">
                  <v-row
                      :key="i"
                      align="center"
                  >
                    <v-col cols="1">
                      <v-list-item-action>
                        <v-checkbox
                            v-model="task.value"
                            color="secondary"
                        />
                      </v-list-item-action>
                    </v-col>

                    <v-col cols="9">
                      <div
                          class="font-weight-light"
                          v-text="task.text"
                      />
                    </v-col>

                    <v-col
                        cols="2"
                        class="text-right"
                    >
                      <v-icon class="mx-1">
                        mdi-pencil
                      </v-icon>
                      <v-icon
                          color="error"
                          class="mx-1"
                      >
                        mdi-close
                      </v-icon>
                    </v-col>
                  </v-row>
                </template>
              </v-card-text>
            </v-tab-item>
          </v-tabs-items>
        </base-material-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
name: "Stat",
  data () {
    return {
      dailySalesChart: {
        data: {
          labels: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
          series: [
            [12, 17, 7, 17, 23, 18, 38],
          ],
        },
        options: {
          lineSmooth: this.$chartist.Interpolation.cardinal({
            tension: 0,
          }),
          low: 0,
          high: 50, // creative tim: we recommend you to set the high sa the biggest typeInputPassword + something for a better look
          chartPadding: {
            top: 0,
            right: 0,
            bottom: 0,
            left: 0,
          },
        },
      },
      dataCompletedTasksChart: {
        data: {
          labels: ['00:00', '3:00', '6:00', '9:00', '12:00', '15:00', '18:00', '21:00'],
          series: [
            [230, 750, 450, 300, 280, 240, 200, 190],
          ],
        },
        options: {
          lineSmooth: this.$chartist.Interpolation.cardinal({
            tension: 0,
          }),
          low: 0,
          high: 1000, // creative tim: we recommend you to set the high sa the biggest typeInputPassword + something for a better look
          chartPadding: {
            top: 0,
            right: 0,
            bottom: 0,
            left: 0,
          },
        },
      },
      emailsSubscriptionChart: {
        data: {
          labels: ['Янв', 'Фев', 'Мар', 'Aпр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
          series: [
            [542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895],
          ],
        },
        options: {
          axisX: {
            showGrid: false,
          },
          low: 0,
          high: 1000,
          chartPadding: {
            top: 0,
            right: 5,
            bottom: 0,
            left: 0,
          },
        },
        responsiveOptions: [
          ['screen and (max-width: 640px)', {
            seriesBarDistance: 5,
            axisX: {
              labelInterpolationFnc: function (value) {
                return value[0]
              },
            },
          }],
        ],
      },
      headers: [
        {
          sortable: false,
          text: '№',
          value: 'id',
        },
        {
          sortable: false,
          text: 'Наименование',
          value: 'name',
        },
        {
          sortable: true,
          text: 'Активные рейсы',
          value: 'salary',
          align: 'right',
        },
        {
          sortable: true,
          text: 'Завершенные рейсы',
          value: 'country',
          align: 'right',
        },
        {
          sortable: false,
          text: 'Город',
          value: 'city',
          align: 'right',
        },
      ],
      items: [
        {
          id: 1,
          name: 'ИП Перевозчиков Т.А.',
          country: '24',
          city: 'Барнаул',
          salary: '769',
        },
        {
          id: 2,
          name: 'ООО "ВезиТолкай"',
          country: '38',
          city: 'Бийск',
          salary: '301',
        },
        {
          id: 3,
          name: 'ООО "КонЁк Горбунок"',
          country: '40',
          city: 'Рубцовск',
          salary: '285',
        },
        {
          id: 4,
          name: 'ИП Чичаев Ч.Ч.',
          country: '25',
          city: 'Заринск',
          salary: '233',
        },
        {
          id: 5,
          name: 'ООО "КоГр"',
          country: '57',
          city: 'Барнаул',
          salary: '384',
        },
      ],
      tabs: 0,
      tasks: {
        0: [
          {
            text: 'Sign contract for "What are conference organizers afraid of?"',
            value: true,
          },
          {
            text: 'Lines From Great Russian Literature? Or E-mails From My Boss?',
            value: false,
          },
          {
            text: 'Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit',
            value: false,
          },
          {
            text: 'Create 4 Invisible User Experiences you Never Knew About',
            value: true,
          },
        ],
        1: [
          {
            text: 'Как сформировать отчеты по завершенным маршрутам за текущий квартал? ООО "ВезиТолкай"',
            value: true,
          },
          {
            text: 'Пользователь забыл пароль. Что делать? ИП Перевозчиков',
            value: false,
          },
        ],
        2: [
          {
            text: 'Как сформировать отчеты по завершенным маршрутам за текущий квартал? ООО "ВезиТолкай"',
            value: false,
          },
          {
            text: 'Пользователь забыл пароль. Что делать? ИП Перевозчиков',
            value: true,
          },
          {
            text: 'Sign contract for "What are conference organizers afraid of?"',
            value: true,
          },
        ],
      },
      list: {
        0: false,
        1: false,
        2: false,
      },
    }
  },
  methods: {
    complete (index) {
      this.list[index] = !this.list[index]
    },
  },
}
</script>

<style scoped>

</style>