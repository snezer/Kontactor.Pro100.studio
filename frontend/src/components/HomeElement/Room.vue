<template>
    <g>
        <rect
                :x="data.positionStart.x"
                :y="data.positionStart.y"
                :width="data.size.width"
                :height="data.size.height"
                :stroke="selectedRoom"
                stroke-width="3"
                fill="#eee"
                fill-opacity=".4"
                class="animate-room"
                :class="{'rent': infoRoom.isForRent}"
                :id="data._id"
        />
        <foreignObject  :x="data.positionStart.x"
                        :y="data.positionStart.y"
                        :width="data.size.width"
                        :height="data.size.height">
          <div class="title-place">
<!--            <div class="icon">-->
<!--              <v-icon x-small color="red" style="border: #F44336 2px solid; border-radius: 50%; padding: 1px">-->
<!--                mdi-keyboard-->
<!--              </v-icon>-->
<!--            </div>-->
            <div class="title">
              {{infoRoom.shortNameOrCode}}
            </div>
          </div>
            <div class="icon-info">
<!--              <div>-->
<!--                <span>-->
<!--                  <img :src="require('@/assets/icon/file.svg')" width="25">-->
<!--                </span>-->
<!--                <span>-->
<!--                  <img :src="require('@/assets/icon/file.svg')" width="25">-->
<!--                </span>-->
<!--              </div>-->
            </div>
<!--          <div class="request">-->
<!--            <v-icon small color="orange darken-4">-->
<!--              mdi-message-->
<!--            </v-icon>-->
<!--          </div>-->
        </foreignObject>
    </g>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    import APICRMServices from "@/services/APICRMServices";
    export default {
        name: "Room",
        props: {
            data: Object,
        },
        data(){
            return{
              infoRoom: {}
            }
        },
        computed:{
            ...mapState('editor',{
                modeEditor: 'modeEditor',
                selectedHomeElement: 'selectedHomeElement'
            }),
            colorStroke(){
                return this.data.newElement ? "red" : "#1565c0"
            },
            selectedRoom(){
                return this.selected ? "green" : this.colorStroke
            },
            selected(){
                return this.data.id == this.selectedHomeElement.id
            },
        },
      watch:{
          async data(){
            await this.getInfoRoom()
          }
      },
        methods:{
            ...mapActions('editor', {
                selectHomeElement: 'select_home_element'
            }),
            async getInfoRoom(){
              const result = await APICRMServices.getInfoRoomByMapId(this.data.id)
              this.infoRoom = result.data
            },
            selectRoom(e){
                e.preventDefault();
                if (this.modeEditor=='draw'){
                    return;
                }
                if (this.data.newElement) {
                    return;
                }
                if (!this.data.newElement) {

                    this.selectHomeElement(this.data)
                    e.stopPropagation();
                }

            }
        },
      mounted() {
        this.getInfoRoom()
      }
    }
</script>

<style scoped lang="sass">
@import "src/sass/color"
.animate-room
  stroke: $blue-dark
  animation: animate-room-stroke 10s linear infinite

@keyframes animate-room-stroke
  0%
    stroke-dasharray: 5

  50%
    stroke-dasharray: 6

  100%
    stroke-dasharray: 5


.room-message
  stroke: #F9954B

.title-place
 display: grid
  grid-template-columns: 1fr 4fr
  gap: 2px

.title-place .icon

.animate-room.rent
  stroke: $red-dark

.title-place .title
  font-size: 1em !important
  line-height: 1em
  padding: 2px

.request
  position: absolute
  bottom: 0
  right: 0

</style>
