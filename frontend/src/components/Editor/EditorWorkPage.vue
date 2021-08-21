<template>
    <div >
        <div @click="onClick">
            <svg>
                <image
                        style=" opacity: 0.5;"
                        :href="'data:image/png;base64,' + selectFloor.image"
                        x="50"
                        y="60"
                        width="1000"
                />
            </svg>
            <svg
                    @mousemove="onMouseMove"
            ><g>

                <home-element-wrapper />
                <polyline :points="path" style="fill:none;stroke:black;stroke-width:3"></polyline>
                <home-element
                        v-show="stageDrawHomeElement!='start'"
                        :data="newHomeElement"
                />
                <node-wrapper />
            </g>
            </svg>
        </div>
    </div>
</template>

<script>
    import NodeWrapper from "@/components/Node/NodeWrapper";
    import HomeElementWrapper from "@/components/HomeElement/HomeElementWrapper";
    import HomeElement from "@/components/HomeElement/HomeElement"
    // import Node from "@/components/Node/Node";
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "EditorWorkPage",
        components: {NodeWrapper,  HomeElementWrapper, HomeElement},
        data(){
            return{
                zoom: 1,
                newHomeElement: null,
                stageDrawHomeElement: 'start',
                drawTypeHomeElement: 'node',
                drawMove: false, //отображать рисованную фигуру только тогда включил рисование сделал клик и повел
                startX: null,
                startY: null,
            }
        },
        computed: {
            ...mapState('editor', {
                modeEditor: 'modeEditor',
                drawTypeElement: 'drawTypeElement',
                selectFloorId: 'selectFloorId',
                floors: 'floors',
                path: 'path'
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
                searchPath: 'search_path'
            }),
            onMouseMove(e) {
                //console.log(this.newHomeElement)
              //const x = e.clientX - 390;
              const x = e.clientX;
                const y = e.clientY-172;

                if (this.drawHomeElement) {
                    this.newHomeElement = {
                        positionStart: {
                            x: Math.min(x, this.startX),
                            y: Math.min(y, this.startY),
                        },
                        size: {
                            width: Math.abs(x - this.startX),
                            height: Math.abs(y - this.startY),
                        },
                        type: this.drawTypeElement,
                        newElement: true,
                    }
                }
            },
            onClick(e) {
              //const x = e.clientX-390;
              const x = e.clientX;
                const y = e.clientY-172;
                //если стоит галка рисования то рисуем объекты
                if (this.drawHomeElement){
                    switch (this.drawTypeElement) {
                        case 'room' :
                        case 'wall' :
                        case 'rack' :
                        case 'chair' :
                        case 'placeforread' :
                        case 'sofa' :
                        case 'pc' :
                        case 'mfu' :
                            {
                            switch (this.stageDrawHomeElement) {
                                case 'start' :
                                    this.startX = x
                                    this.startY = y
                                    this.newHomeElement = {
                                        positionStart: {
                                            x: x,
                                            y: y,
                                        }
                                    }
                                    this.stageDrawHomeElement = 'end'
                                    break;
                                case 'end' :
                                    this.addHomeElement({
                                        positionStart: this.newHomeElement.positionStart,
                                        size: {
                                            width: Math.abs(x  - this.startX),
                                            height: Math.abs(y - this.startY),
                                        },
                                        type: this.drawTypeElement,
                                    })
                                    this.startX = null;
                                    this.startY = null;
                                    this.stageDrawHomeElement = 'start'

                            }
                            break
                        }
                        case 'door' :
                        case 'stair' :
                            case 'elevator' : {
                            this.addNode({
                                positionStart: {
                                    x: x,
                                    y: y,
                                }
                            })

                            break
                        }
                    }

                } else { //иначе выбираем их для редактирования
                    this.unselect()
                }
            }
        },
        mounted() {
            this.getAllHomeElements()
        }
    }
</script>

<style scoped>
    scheme{
        opacity: 0.3;
        filter: invert(0);
    }
    svg {
        position: absolute;
        top: 30px;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 0;
    }
</style>
