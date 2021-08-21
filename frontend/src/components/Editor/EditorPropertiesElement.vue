<template>
  <v-card flat>
    <v-tabs v-model="tabInfoModel">
      <v-tabs-slider color="blue"></v-tabs-slider>
      <v-tab key="stat">
        Статистика
      </v-tab>
      <v-tab key="company">
        О компании
      </v-tab>
    </v-tabs>
    <v-tabs-items v-model="tabInfoModel">
      <v-tab-item key="stat" style="padding: 10px">
        <v-row>
          <v-col sm="12">
              <v-row align-content="center" justify="center">
                <v-col class="text-center">
                  <v-badge overlap bordered content="3" color="green">
                  <v-btn icon small>
                      <v-icon color="">
                        mdi-bell-outline
                      </v-icon>
                  </v-btn>
                  </v-badge>
                  <br>
                  Входящие
                </v-col>
                <v-col class="text-center">
                  <v-badge overlap bordered content="1" color="red">
                    <v-btn icon small>
                      <v-icon>
                        mdi-alert-circle-outline
                      </v-icon>
                    </v-btn>
                  </v-badge>
                  <br>
                  Запросы
                </v-col>
              </v-row>
          </v-col>
          <v-col sm="12">
            <v-card flat>
              <v-card-title>Календарь</v-card-title>
              <v-subheader>
                На сегодня нет дел
              </v-subheader>
            </v-card>
          </v-col>
        </v-row>
        <v-row>
            <v-subheader class="subtitle-1">
              Информация о объекте
            </v-subheader>
          <v-col cols="12">
            <v-text-field
                dense
                v-model="infoRoom.shortNameOrCode"
                label="Наименование"/>
          </v-col>
          <v-col
              cols="12">
            <v-textarea
                dense
                rows="3"
                v-model="infoRoom.name"
                label="Описание"/>
          </v-col>
          <v-col cols="12">
            <v-text-field
                dense
                v-model="infoRoom.maxPeopleNumber"
                label="Количество сотрудников"/>
          </v-col>
          <v-col cols="12">
            <v-text-field
                dense
                v-model="infoRoom.area"
                label="Площадь"/>
          </v-col>
          <v-col cols="12">
            <v-switch dense label="Для долгосрочной аренды" v-model="infoRoom.isForLongTemRent"></v-switch>
          </v-col>
          <v-col cols="12">
            <v-switch dense label="Арендовано" v-model="infoRoom.isForRent"></v-switch>
          </v-col>
          <v-col cols="12">
            <v-switch dense label="В ремонте"></v-switch>
          </v-col>
          <v-col
              cols="12">
            <v-checkbox
                label="Только для персонала"/>
          </v-col>
        </v-row>
        <v-row v-show="publicMap">
          <v-col>
            <v-btn @click="deleteElement" class="error">
              Удалить
            </v-btn>
          </v-col>
          <v-col>
            <v-btn :disabled="this.selectNode" @click="saveInfoRoom" class="primary">
              Сохранить
            </v-btn>
          </v-col>
        </v-row>
      </v-tab-item>
      <v-tab-item key="company" style="padding: 10px">
        <v-card flat>
          <v-row style="margin-top: 25px">
            <v-col cols="12">
              <span class="subtitle-2">Общие данные</span>
            </v-col>
            <v-col lg="12">
              <v-text-field dense label="Наименование"></v-text-field>
            </v-col>
            <v-col lg="12">
              <v-text-field dense label="Вид деятельности"></v-text-field>
            </v-col>
            <v-col lg="12">
              <v-text-field dense label="Юр. адрес"></v-text-field>
            </v-col>
            <v-col lg="12">
              <v-text-field dense label="Почтовый адрес"></v-text-field>
            </v-col>
            <v-col cols="12">
              <span class="subtitle-2">Экономическая информация</span>
            </v-col>
            <v-col lg="6">
              <v-text-field dense label="ИНН"></v-text-field>
            </v-col>
            <v-col lg="6">
              <v-text-field dense label="Банк"></v-text-field>
            </v-col>
            <v-col lg="6">
              <v-text-field dense label="БИК"></v-text-field>
            </v-col>
            <v-col lg="6">
              <v-text-field dense label="Рас./счёт"></v-text-field>
            </v-col>
            <v-col lg="6">
              <v-text-field dense label="Корр./счёт"></v-text-field>
            </v-col>
          </v-row>
        </v-card>
      </v-tab-item>
    </v-tabs-items>
  </v-card>
</template>

<script>
    import {mapState, mapActions} from 'vuex'
    import APICRMServices from "@/services/APICRMServices";
    export default {
        name: "EditorPropertiesElement",
      props:{
          publicMap: Boolean,
      },
        data(){
            return{
              saveInfoRoomLoading: false,
              tabInfoModel: 'stat',
              name:'',
              data: {},
              infoRoom:{
                name: '',
                shortNameOrCode: '',
                isForRent: true,
                isForLongTemRent: true,
                area: 0,
                mapRoomId:'',
                maxPeopleNumber: 0,
              }
            }
        },
        computed:{
            ...mapState('editor', {
                selectedNode: 'selectedNode',
                selectedHomeElement: 'selectedHomeElement',
                selectHomeElement: 'selectHomeElement',
                selectNode: 'selectNode',
                nodeElements: 'nodeElements',
                homeElements: 'homeElements',
            })
        },
      watch:{
          selectHomeElement(){
            this.getInfoRoom()
          }
      },
        methods:{
            ...mapActions('editor', {
                deleteHomeElement: 'delete_home_element',
                deleteNode: 'delete_node',
                getAllNodeElements: 'get_all_node_elements',
                addHomeElement: 'add_home_element',
                addNode: 'add_node',
                unselect: 'unselect'
            }),
            deleteElement(){
                if (this.selectHomeElement){
                    this.deleteHomeElement(this.selectedHomeElement)
                } else if (this.selectNode){
                    this.deleteNode(this.selectedNode)
                }
            },
            async save(){
                if (this.selectHomeElement){
                    this.data = Object.assign( this.selectedHomeElement, this.data)
                    await this.addHomeElement(this.data)
                    this.data = {}
                }
                this.unselect()
            },
            async saveInfoRoom(){
              this.infoRoom.mapRoomId = this.selectedHomeElement.id
              this.infoRoom.area = Number(this.infoRoom.area)
              this.infoRoom.maxPeopleNumber = Number(this.infoRoom.maxPeopleNumber)
              this.saveInfoRoomLoading = true
              const result  = await APICRMServices.saveInfoRoom(this.infoRoom)
              this.saveInfoRoomLoading = false
            },
            async getInfoRoom(){
              this.infoRoom ={}
              const infoRoom = await APICRMServices.getInfoRoomByMapId(this.selectedHomeElement.id)
              this.infoRoom = infoRoom.data
            }
        },
        mounted() {

        }
    }
</script>

<style scoped>

</style>
