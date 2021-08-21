<template>
    <div>
       <Editor public-map="true"></Editor>
    </div>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    import HomeElementWrapper from "@/components/HomeElement/HomeElementWrapper";
    import NodeWrapper from "@/components/Node/NodeWrapper";
    import Editor from "@/views/Editor";
    export default {
        name: "MapForUser",
        components: {Editor},
        data(){
            return{
                items: [
                    'К/Х-АТС',
                    'К/Х1',
                    'ИСК',
                    'ИСК-НС',
                    'ОГЛ',
                    'БИБЛИОГРАФ'
                ]
            }
        },
        computed: {
            ...mapState('editor', {
                modeEditor: 'modeEditor',
                drawTypeElement: 'drawTypeElement',
                selectFloorId: 'selectFloorId',
                floors: 'floors'
            }),
            drawHomeElement(){
                return this.modeEditor==='draw'
            },
            selectFloor(){
                return this.floors.find(n=>n.id==this.selectFloorId)
            }
        },
        methods: {
            ...mapActions('editor', {
                addHomeElement: 'add_home_element',
                addNode: 'add_node',
                unselect: 'unselect',
                getAllHomeElements: 'get_all_home_elements',
            }),
        },
        mounted() {
            this.getAllHomeElements()
        }
    }
</script>

<style scoped>
        /*svg {*/
        /*    position: absolute;*/
        /*    top: 100px;*/
        /*    left: 30px;*/
        /*    width: 100%;*/
        /*    height: 100%;*/
        /*    z-index: 0;*/
        /*}*/
</style>
