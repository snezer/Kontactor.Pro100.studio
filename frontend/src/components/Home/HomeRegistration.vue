<template>
  <div style="width: 100%; height: 100%">
    <v-stepper vertical class="wrapper-registration" v-model="stepRegistration" color="white">
      <div class="steps">
        <div class="title">
          <v-img width="50%" :src="require('@/assets/logo (3).svg')"></v-img>
        </div>
        <div class="description">
          Начнем работать
        </div>
        <v-stepper-step
            :complete="stepRegistration > 1"
            step="1"
        >
          <span class="white--text">Персональные данные</span>
          <small class="white--text">Личные данные пользователя</small>
        </v-stepper-step>
        <v-stepper-step
            :complete="stepRegistration > 2"
            step="2"
        >
          <span class="white--text">Данные площадки</span>
          <small class="white--text">Информация о планируемой площадке</small>
        </v-stepper-step>
        <v-stepper-step
            :complete="stepRegistration > 3"
            step="3"
        >
          <span class="white--text">Место площадки</span>
          <small class="white--text">Выберите расположение на свободных местах</small>
        </v-stepper-step>
        <v-stepper-step
            :complete="stepRegistration > 4"
            step="4"
        >
          <span class="white--text">Финиш</span>
          <small class="white--text"></small>
        </v-stepper-step>
      </div>
      <div class="steps-content">
        <div class="text-right">
          <v-btn icon link to="/">
            <v-icon>
              mdi-close
            </v-icon>
          </v-btn>
        </div>
        <v-stepper-content step="1">
          <v-card
              flat
              class="form"
              height="100%"
          >
            <h1 class="title-form">Администратор площадки</h1>
            <h3 class="description-form">Укажиет свои персональные данные. Они понадобятся для составления документации при заключении договора</h3>
            <v-row style="margin-top: 25px">
              <v-col cols="12">
                <span class="subtitle-2">Персональные данные</span>
              </v-col>
              <v-col lg="4">
                <v-text-field dense label="Фамилия"></v-text-field>
              </v-col>
              <v-col lg="4">
                <v-text-field dense label="Имя"></v-text-field>
              </v-col>
              <v-col lg="4">
                <v-text-field dense label="Отчество"></v-text-field>
              </v-col>
              <v-col cols="12">
                <span class="subtitle-2">Паспорт</span>
              </v-col>
              <v-col lg="1">
                <v-text-field dense label="Серия"></v-text-field>
              </v-col>
              <v-col lg="2">
                <v-text-field dense label="Номер"></v-text-field>
              </v-col>
              <v-col lg="2">
                <v-text-field dense label="Дата выдачи"></v-text-field>
              </v-col>
              <v-col lg="7">
                <v-text-field dense label="Место выдачи"></v-text-field>
              </v-col>
              <v-col lg="7">
                <v-text-field dense label="Место прописки"></v-text-field>
              </v-col>
              <v-col lg="7">
                <v-text-field dense label="Место проживания" disabled></v-text-field>
              </v-col>
              <v-col lg="5">
                <v-switch style="margin-top: 12px;" label=" Совпадает с местом прописки"></v-switch>
              </v-col>
            </v-row>
            <div class="action-form">
              <v-row>
                <v-col cols="6">
                  <v-btn color="primary" large rounded outlined>
                    Назад
                  </v-btn>
                </v-col>
                <v-col cols="6" class="text-right">
                  <v-btn
                      class="text-right"
                      elevation="0"
                      width="150"
                      rounded
                      large
                      color="primary"
                      @click="stepRegistration = 2"
                  >
                    Далее
                  </v-btn>
                </v-col>
              </v-row>


            </div>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card
              flat
              class="form"
              height="100%"
          >
            <h1 class="title-form">Информация о площадке</h1>
            <h3 class="description-form">Укажиет данные площадки. Они понадобятся для составления документации при заключении договора</h3>
            <v-row style="margin-top: 25px">
              <v-col cols="12">
                <span class="subtitle-2">Общие данные</span>
              </v-col>
              <v-col lg="6">
                <v-text-field dense label="Наименование"></v-text-field>
              </v-col>
              <v-col lg="6">
                <v-text-field dense label="Вид деятельности"></v-text-field>
              </v-col>
              <v-col lg="6">
                <v-text-field dense label="Юр. адрес"></v-text-field>
              </v-col>
              <v-col lg="6">
                <v-text-field dense label="Почтовый адрес"></v-text-field>
              </v-col>
              <v-col cols="12">
                <span class="subtitle-2">Экономическая информация</span>
              </v-col>
              <v-col lg="3">
                <v-text-field dense label="ИНН"></v-text-field>
              </v-col>
              <v-col lg="3">
                <v-text-field dense label="Банк"></v-text-field>
              </v-col>
              <v-col lg="3">
                <v-text-field dense label="БИК"></v-text-field>
              </v-col>
              <v-col cols="1">

              </v-col>
              <v-col lg="3">
                <v-text-field dense label="Рас./счёт"></v-text-field>
              </v-col>
              <v-col lg="3">
                <v-text-field dense label="Корр./счёт"></v-text-field>
              </v-col>
            </v-row>
            <div class="action-form">
              <v-row>
                <v-col cols="6">
                  <v-btn color="primary" large rounded outlined>
                    Назад
                  </v-btn>
                </v-col>
                <v-col cols="6" class="text-right">
                  <v-btn
                      class="text-right"
                      elevation="0"
                      width="150"
                      rounded
                      large
                      color="primary"
                      @click="stepRegistration = 3"
                  >
                    Далее
                  </v-btn>
                </v-col>
              </v-row>
            </div>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="3">
          <v-card flat height="850">
            <MapForUser></MapForUser>
          </v-card>
          <v-card flat height="350">
            <div>
              <v-row>
                <v-col
                    cols="12"
                    sm="6"
                >
                  <span style="font-size: 1.3em">Укажите время аренды</span>
                  <v-date-picker
                      v-model="dates"
                      landscape
                      range
                  ></v-date-picker>
                </v-col>
                <v-col
                    cols="12"
                    sm="6"
                >
                  <span style="font-size: 1.3em">Была выбранна площадка: {{activeRoom.data.shortNameOrCode}}</span>
                  <v-row>
                    <v-col cols="6">
                      <v-btn color="primary" large rounded outlined>
                        Назад
                      </v-btn>
                    </v-col>
                    <v-col cols="6" class="text-right">
                      <v-btn
                          class="text-right"
                          elevation="0"
                          width="150"
                          rounded
                          large
                          color="primary"
                          @click="stepRegistration = 4"
                      >
                        Подать заявку
                      </v-btn>
                    </v-col>
                  </v-row>
                </v-col>
              </v-row>
            </div>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="4">
          <v-card flat>
            <v-card-title>
              Ваша заявка была принята на рассмотрение! Ожидайте, с вами свяжутся орагнизаторы.
            </v-card-title>
          </v-card>
        </v-stepper-content>
      </div>
    </v-stepper>
  </div>
</template>

<script>
import MapForUser from "@/components/MapForUser";
import {mapState} from "vuex";
export default {
  name: "HomeRegistration",
  components: {MapForUser},
  data(){
    return{
      dates: ['2019-09-10', '2019-09-20'],
      stepRegistration: 1,
      activeRoom: {
        data:{
          shortNameOrCode: ''
        }
      },
    }
  },
  computed:{
    ...mapState('editor',{
      selectHomeElement: 'selectedHomeElement'
    }),
    dateRangeText () {
      return this.dates.join(' ~ ')
    },
  },
  watch:{
    selectHomeElement: function (val){
      this.activeRoom = this.selectHomeElement
    }
  }
}
</script>

<style lang="sass">
@import "src/sass/color"

.wrapper-registration
    width: 100%
    height: 100%
    display: grid
    grid-template-columns: 3fr 8fr
    padding-bottom: 0 !important
.steps
    grid-area: 1 / 1 / span 1 / span 1
    background-color: $blue-dark
    display: flex
    flex-direction: column
    justify-content: start
    align-items: flex-start
    padding: 25px
.steps .title
  font-size: 1.5em !important
  color: white
  display: flex
  justify-content: start
  align-items: center
.steps .description
  color: white
  font-size: 1.3em
  margin-top: 50px
.steps-content
    grid-area: 1 / 2 / span 1 / span 1
    width: 80%
    margin: 0 auto
.form
  padding-top: 100px
  height: 100%
.title-form
  color: $black-dark !important
  font-size: 2.5em !important
  text-align: center !important
.description-form
  width: 50%
  margin: 25px auto
  color: $black-dark !important
  font-size: 1em !important
  text-align: center !important
.action-form
  padding-right: 50px
</style>