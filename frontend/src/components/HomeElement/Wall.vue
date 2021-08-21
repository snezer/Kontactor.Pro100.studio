<template>
    <g>
        <rect
                :x="data.positionStart.x"
                :y="data.positionStart.y"
                :width="data.size.width"
                :height="data.size.height"
                :stroke="selectedRoom"
                stroke-width="2"
                stroke-dasharray="10"
                :fill="colorFiil"
                fill-opacity=".4"
        />
        <foreignObject  :x="data.positionStart.x"
                        :y="data.positionStart.y">
<!--                <img width="10" height="10" :src="require(pathIcon)"/>-->
        </foreignObject>
    </g>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "Wall",
        props: {
            data: Object
        },
        data(){
            return{

            }
        },
        computed:{
            ...mapState('editor',{
                modeEditor: 'modeEditor',
                selectedHomeElement: 'selectedHomeElement'
            }),
            colorStroke(){
                return this.data.newElement ? "red" : "#584B01"
            },
            selectedRoom(){
                return this.selected ? "green" : this.colorStroke
            },
            selected(){
                return this.data.id == this.selectedHomeElement.id
            },
            colorBorder(){
                return '#584B01'
            },
            colorFiil(){
                return '#BAA635'
            },
            pathIcon(){
                let path = ''
                switch (this.data.type) {
                    case 'rack' : path = 'rack.svg'
                        break;
                    case 'chair' : path = 'chair.svg'
                        break;
                    case 'sofa' : path = 'sofa.svg'
                        break;
                    case 'pc' : path = 'pc.svg'
                        break;
                    case 'mfu' : path = 'mfu.svg'
                        break;
                }
                return path
            }

        },
        methods:{
            ...mapActions('editor', {
                selectHomeElement: 'select_home_element'
            }),
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
        }
    }
</script>

<style scoped>

</style>
