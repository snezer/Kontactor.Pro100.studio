<template>
  <div class="text-right">
    <v-btn v-for="mode in modes"
           icon small @click="setMode(mode.event)"
           :title="mode.title"
           :key="mode.event">
      <v-icon small>
        {{`mdi-${mode.icon}`}}
      </v-icon>
    </v-btn>
    <br>
    <v-btn-toggle v-model="btnHomeElements" background-color="grey lighten-4"  dense active-class="blue white-text" v-show="activeElements">
      <v-btn v-for="(homeElement, i) in homeElements"
             :key="homeElement.value"
             :title="homeElement.title"
             @click.native="setDrawTypeElement(homeElement.value)" icon small>
  <!--       <img :src="require('@/assets/icon/white/'+homeElement.icon)" width="10">-->
         <v-icon color="grey darken-2" :class="{'white': btnHomeElements==i}">
           {{homeElement.fontIcon}}
         </v-icon>
      </v-btn>
    </v-btn-toggle>
  </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
name: "EditorMode",
  data(){
    return{
      btnHomeElements: undefined,
      btnNodeElements: undefined,
      modes: [
        {
          title: 'Рисование',
          icon: 'circle-edit-outline',
          event: 'draw'
        },
        {
          title: 'Выбор объекта',
          icon: 'cursor-pointer',
          event: 'select'
        },

      ],
      homeElement: '1',
      homeElements: [
        {
          title: 'Комната',
          icon: 'room.svg',
          fontIcon: 'mdi-vector-rectangle',
          value: 'room'
        },
        {
          title: 'Стеллаж',
          icon: 'rack.svg',
          fontIcon: 'mdi-wardrobe-outline',
          value: 'rack'
        },
        {
          title: 'Кафедра',
          icon: 'chair.svg',
          fontIcon: 'mdi-chair-school',
          value: 'chair'
        },
        {
          title: 'Место для чтения',
          icon: 'placeforread.svg',
          fontIcon: 'mdi-book-outline',
          value: 'placeforread'
        },
        {
          title: 'Диван',
          icon: 'sofa.svg',
          fontIcon: 'mdi-sofa',
          value: 'sofa'
        },
        {
          title: 'Стол с ПК',
          icon: 'pc.svg',
          fontIcon: 'mdi-desk',
          value: 'pc'
        },
        {
          title: 'МФУ',
          icon: 'mfu.svg',
          fontIcon: 'mdi-printer',
          value: 'mfu'
        },
        {
          title: 'Дверь',
          icon: 'door.svg',
          fontIcon: 'mdi-door',
          value: 'door'
        },
        {
          title: 'Лестница',
          icon: 'stair.svg',
          fontIcon: 'mdi-stairs',
          value: 'stair'
        },
        {
          title: 'Лифт',
          icon: 'elevator.svg',
          fontIcon: 'mdi-elevator-passenger',
          value: 'elevator'
        }
      ]
    }
  },
  computed: {
    ...mapState('editor', {
      drawTypeElement: 'drawTypeElement',
      modeEditor: 'modeEditor'
    }),
    activeElements(){
      return this.modeEditor=='draw'
    }
  },
  methods: {
    setMode(type){
      if(type=='draw') {
        this.setDrawMode()
      }
      else{
        this.setSelectMode()
      }
    },
    ...mapActions('editor', {
      setDrawMode: 'set_draw_mode_editor',
      setSelectMode: 'set_select_mode_editor',
      setDrawTypeElement: 'set_draw_type_element',
    })
  }
}
</script>

<style scoped>

</style>