<template>
    <div class="воттак">
      <v-toolbar flat color="grey lighten-4">
        <v-toolbar-title>Карта</v-toolbar-title>
        <v-spacer/>
        <v-toolbar-items>
          <editor-mode v-show="!this.publicMap" />
          <v-divider vertical />
          <editor-floors  show-btn-edit="true" />
        </v-toolbar-items>
      </v-toolbar>
      <div class="mainWindow">
        <editor-work-page />
      </div>
      <v-navigation-drawer app right width="450" style="z-index: 1001" v-model="activeRightBar">
        <editor-properties-element :public-map="!this.publicMap" />
      </v-navigation-drawer>
    </div>
</template>

<script>
    import EditorWorkPage from "@/components/Editor/EditorWorkPage";
    import {mapState} from 'vuex'
    import EditorPropertiesElement from "../components/Editor/EditorPropertiesElement";
    import EditorFloors from "../components/Editor/EditorFloors";
    import EditorMode from "@/components/Editor/EditorMode";
    export default {
        name: "Editor",
        components: {EditorMode, EditorFloors, EditorPropertiesElement, EditorWorkPage},
      props:{
        publicMap: Boolean
      },
        data: () => ({
            drawer: true,
            drawerRight: false,
            items: [
                { title: 'Home', icon: 'dashboard' },
                { title: 'About', icon: 'question_answer' },
            ],
        }),
        computed:{
            ...mapState('editor', {
                selectNode: 'selectNode',
                selectedNode: 'selectedNode',
                selectHomeElement: 'selectHomeElement',
                selectedHomeElement: 'selectedHomeElement',
                modeEditor: 'modeEditor'
            }),
            activeRightBar:{
                get(){
                    return  this.selectHomeElement || this.selectNode
                },
                set(){
                    return
                }
            },
            modeEditorText(){
                return this.modeEditor=='draw' ? 'Режим рисования' : 'Режим выбора объектов'
            }
        },
        created () {
            this.$vuetify.theme.dark = false
        }
    }
</script>

<style scoped>
    .mainWindow {
      position: relative;
        width: 100%;
        height: 100vh;
        z-index: 0;
    }
    .v-main__wrap{
        padding: 0;
    }
</style>
