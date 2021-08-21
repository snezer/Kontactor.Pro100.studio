<template>
  <v-card flat style="padding: 10px">
        <v-row>
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
        <v-row v-show="!publicMap">
          <v-col>
            <v-btn block outlined color="primary" link to="/registration">
              Арендовать
            </v-btn>
          </v-col>
        </v-row>
        <v-row>
          <v-col lg="4">
            <v-menu
                ref="menu"
                v-model="menu"
                :close-on-content-click="false"
                :return-value.sync="startDate"
                transition="scale-transition"
                offset-y
                min-width="auto"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                    v-model="startDate"
                    label="Начало аренды"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                  v-model="startDate"
                  no-title
                  scrollable
              >
                <v-spacer></v-spacer>
                <v-btn
                    text
                    color="primary"
                    @click="menu = false"
                >
                  Закрыть
                </v-btn>
                <v-btn
                    text
                    color="primary"
                    @click="$refs.menu.save(startDate)"
                >
                  OK
                </v-btn>
              </v-date-picker>
            </v-menu>
          </v-col>
          <v-col lg="4">
            <v-menu
                ref="menu"
                v-model="menu1"
                :close-on-content-click="false"
                :return-value.sync="endDate"
                transition="scale-transition"
                offset-y
                min-width="auto"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                    v-model="endDate"
                    label="Конец аренды"
                    prepend-icon="mdi-calendar"
                    readonly
                    v-bind="attrs"
                    v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                  v-model="endDate"
                  no-title
                  scrollable
              >
                <v-spacer></v-spacer>
                <v-btn
                    text
                    color="primary"
                    @click="menu1 = false"
                >
                  Закрыть
                </v-btn>
                <v-btn
                    text
                    color="primary"
                    @click="$refs.menu.save(endDate)"
                >
                  OK
                </v-btn>
              </v-date-picker>
            </v-menu>
          </v-col>
          <v-col>
            <v-btn outlined block color="red" @click="createRents">
              Арендовать
            </v-btn>
          </v-col>
        </v-row>
        <v-row v-show="isUkPersonnel">
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
  </v-card>
</template>

<script>
import {mapState, mapActions, mapGetters} from 'vuex'
    import APICRMServices from "@/services/APICRMServices";
    export default {
        name: "EditorPropertiesElement",
      props:{
          publicMap: Boolean,
      },
        data(){
            return{
              startDate: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
              endDate: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
              menu: false,
              menu1: false,
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
            }),
            ...mapGetters('user',{
                isUkPersonnel: 'isUKPersonnel',
                tenantId: 'tenantId',
                userId: 'userId'
            })
        },
      watch:{
          async selectHomeElement(){
            console.log(1)
            await this.getInfoRoom()
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
          async createRents(){
              const result = await APICRMServices.createRents({
                tenantId: this.tenantId,
                compartmentId: this.infoRoom.id,
                from: this.startDate,
                to: this.endDate
              })
              if (result) {
                console.log(result)
              }
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
              this.infoRoom ={name: '',
                shortNameOrCode: '',
                isForRent: false,
                isForLongTemRent: true,
                area: 0,
                mapRoomId:'',
                maxPeopleNumber: 0,}
                const infoRoom1 = await APICRMServices.getInfoRoomByMapId(this.selectedHomeElement.id)
                if (typeof (infoRoom1.data)=='object'){
                  this.infoRoom = infoRoom1.data
                }


            }
        },
        mounted() {

        }
    }
</script>

<style scoped>

</style>
