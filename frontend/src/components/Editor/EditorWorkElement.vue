<template>
    <v-navigation-drawer>
        <v-subheader>
            Режимы работы
        </v-subheader>
        <v-list dense
                shaped
        >
            <v-list-item-group v-model="modeEditors" color="primary">
                <v-list-item
                        v-for="mode in modes"
                        @click="setMode(mode.event)"
                        :key="mode.event"
                >
                    <v-list-item-action>
                        <v-icon class="white--text">{{mode.icon}}</v-icon>
                    </v-list-item-action>
                    <v-list-item-content>
                        <v-list-item-title class="text-left white--text">{{mode.title}}</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list-item-group>
        </v-list>
        <v-subheader>
            Элементы здания
        </v-subheader>
        <v-list shaped  :disabled="modeEditor!='draw'">
            <v-list-item-group v-model="homeElement" color="warning" class="white--text">
                <v-list-item
                        link
                        v-for="(homeElement, i) in homeElements"
                        :key="i"
                        @click="setDrawTypeElement(homeElement.value)"
                >
                    <v-list-item-action>
                        <v-avatar>
                            <img :src="require('@/assets/icon/white/'+homeElement.icon)" alt="">
                        </v-avatar>
                    </v-list-item-action>
                    <v-list-item-content>
                        <v-list-item-title class="text-left white--text">{{homeElement.title}}</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
                <v-subheader>
                    Элементы переходов
                </v-subheader>
                <v-divider />
                <v-list-item
                        link
                        v-for="(element, i) in nodeElements"
                        :key="i+homeElements.length"
                        @click="setDrawTypeElement(element.value)"
                >
                    <v-list-item-action>
                        <v-avatar>
                            <img :src="require('@/assets/icon/white/'+element.icon)" alt="">
                        </v-avatar>
                    </v-list-item-action>
                    <v-list-item-content>
                        <v-list-item-title class="text-left white--text">{{element.title}}</v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list-item-group>

        </v-list>

        <v-list shaped dense :disabled="modeEditor!='draw'">
            <v-list-item-group v-model="homeElement" color="error">

            </v-list-item-group>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
    import {mapActions, mapState} from 'vuex'
    export default {
        name: "EditorWorkElement",
        data(){
            return{
                modeEditors: '1',
                modes: [
                    {
                        title: 'Рисование',
                        icon: 'edit',
                        event: 'draw'
                    },
                    {
                        title: 'Выбор объекта',
                        icon: 'check',
                        event: 'select'
                    },

                ],
                homeElement: '1',
                homeElements: [
                    {
                        title: 'Комната',
                        icon: 'room.svg',
                        value: 'room'
                    },
                    {
                        title: 'Стеллаж',
                        icon: 'rack.svg',
                        value: 'rack'
                    },
                    {
                        title: 'Кафедра',
                        icon: 'chair.svg',
                        value: 'chair'
                    },
                    {
                        title: 'Место для чтения',
                        icon: 'placeforread.svg',
                        value: 'placeforread'
                    },
                    {
                        title: 'Диван',
                        icon: 'sofa.svg',
                        value: 'sofa'
                    },
                    {
                        title: 'Стол с ПК',
                        icon: 'pc.svg',
                        value: 'pc'
                    },
                    {
                        title: 'МФУ',
                        icon: 'mfu.svg',
                        value: 'mfu'
                    },
                ],
                nodeElement: '',
                nodeElements: [
                    {
                        title: 'Дверь',
                        icon: 'door.svg',
                        value: 'door'
                    },
                    {
                        title: 'Лестница',
                        icon: 'stair.svg',
                        value: 'stair'
                    },
                    {
                        title: 'Лифт',
                        icon: 'elevator.svg',
                        value: 'elevator'
                    }
                ]
            }
        },
        computed: {
            ...mapState('editor', {
                drawTypeElement: 'drawTypeElement',
                modeEditor: 'modeEditor'
            })
        },
        methods: {
            setMode(type){
                if(type=='draw') {
                    this.setDrawMode()
                }
                else if(type=='select'){
                    this.setSelectMode()
                }else{
                    this.setSearcheMode()
                }
            },
            ...mapActions('editor', {
                setDrawMode: 'set_draw_mode_editor',
                setSelectMode: 'set_select_mode_editor',
                setDrawTypeElement: 'set_draw_type_element',
                setSearcheMode: 'set_searche_mode',
            })
        }
    }
</script>

<style scoped>

</style>
