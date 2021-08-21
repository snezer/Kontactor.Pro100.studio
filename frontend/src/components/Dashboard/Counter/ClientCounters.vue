<style scoped>
	.header-label {
		margin-left: 20px;
	}
</style>

<template>
	<div class="component-root">
		<h2 class="header-label">Информация о показаниях счетчиков</h2>
		<v-card class="mx-auto">
			<v-card-text>
      			<div>Данные ресурсопотребления</div>
      			<p class="text-h4 text--primary">Помещение 201</p>
				<div class="text--primary">
					<v-data-table
					 :headers="headers"
					 :items="counters"
					 :items-per-page="-1"
					 class="elevation-1"
					 >
						<template v-slot:item.currentVolume="props">
							<v-edit-dialog
							:return-value.sync="props.item.currentVolume"
							@save="save"
							@cancel="cancel"
							@open="open"
							@close="close"
							>
							{{ props.item.currentVolume }}
							<template v-slot:input>
								<v-text-field
								v-model="props.item.currentVolume"								
								label="Текущие"
								single-line
								counter
								></v-text-field>
							</template>
							</v-edit-dialog>
						</template>
					</v-data-table>
				</div>
    		</v-card-text>
		</v-card>

		<v-card class="mx-auto">
			<v-card-text>
      			<div>Данные ресурсопотребления</div>
      			<p class="text-h4 text--primary">Помещение 202</p>
				<div class="text--primary">
					<v-data-table></v-data-table>
				</div>
    		</v-card-text>
		</v-card>
	</div>

</template>

<script>

export default {
	name:"ClientCounters",
	 data(){
		return{
			headers : [
					{text:"Счетчик", value: "number"},
					{text:"Вид", value: "cType"},
					{text:"Прошлое", value: "volumeLast"},
					{text:"Текущее показание", value: "currentVolume"}
				],

			counters:[
					{number:"ГВ10290-178", cType:"ГВC", volumeLast:1235.6, currentVolume:1664.4},
					{number:"XВ12293-213", cType:"XВC", volumeLast:1235.6, currentVolume:1664.4},
					{number:"ГВ10290-178", cType:"ГВC", volumeLast:1235.6, currentVolume:1664.4},
					{number:"ГВ10290-178", cType:"ГВC", volumeLast:1235.6, currentVolume:1664.4}
				]
		}
  }
	
}
</script>
