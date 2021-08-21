<template>
    <div>
      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn
              v-bind="attrs"
              v-on="on"
              small
              depressed

          >
            этажи
            <v-icon x-small right>
              mdi-menu-down
            </v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item
              @click="setActiveFloor(floor.id)"
              small
              v-for="floor in floors"
              :key="floor.id"
          >
            <v-list-item-title>{{ floor.title }} этаж</v-list-item-title>
          </v-list-item>
          <v-list-item @click="addFloor({title: '3', value: 0})">
            <v-list-item-title>+ этаж</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
        <v-btn icon v-show="showBtnEdit" small @click="onButtonClick">
          <input
              accept="image/png, image/jpeg, image/bmp"
              multiple
              type="file"
              class="d-none"
              id="filesEducation"
              ref="files"
              @change="handleFilesEducationUpload"
          />
          <v-icon small>mdi-image-plus</v-icon>
        </v-btn>
        <v-btn  v-show="showBtnEdit"
                small
                icon
                @click="deleteFloor(activeFloorId)"
        >
          <v-icon small>
            mdi-delete
          </v-icon>
        </v-btn>
    </div>

</template>

<script>
    import {mapState, mapActions} from 'vuex'
    export default {
        name: "EditorFloors",
        data(){
            return{
                filesEdu: '',
                activeBtn: 1,
              isSelecting: false,
            }
        },
        props:{
          showBtnEdit: Boolean
        },
        computed:{
            ...mapState('editor', {
                floors: 'floors',
                activeFloorId: 'activeFloorId'
            }),
          nameMenu(){
              return this.floors.find(floor => floor.Id==this.activeFloorId).title
          }
        },
        methods:{
            ...mapActions('editor', {
                setActiveFloor: 'set_active_floor',
                addFloor: 'add_floor',
                getFloors: 'get_all_floors',
                addImage: 'add_image_for_floor',
                deleteFloor: 'delete_floor'
            }),
          onButtonClick() {
            this.isSelecting = true
            window.addEventListener('focus', () => {
              this.isSelecting = false
            }, { once: true })

            this.$refs.files.click()
          },
            handleFilesEducationUpload(e) {
              const files = e.target.files
                let formData = new FormData()
                console.log(files)
                for (let i = 0; i < files.length; i++) {
                    let file = files[i]
                    formData.append('files', file)
                }
                this.filesEdu = formData
                this.addImage(formData)
            },
        },
        mounted() {
            this.getFloors()
        }
    }
</script>

<style scoped>

</style>
