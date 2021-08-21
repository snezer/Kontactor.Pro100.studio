<template>
  <div class="counter-wrapper">
    <v-row>
      <v-col lg="4">
        <v-card flat>
          <v-card-title>Передать показания</v-card-title>
          <v-row>
            <v-col cols="5" offset="1">
              <v-text-field type="number" label="Электроэнергия" append-icon="mdi-send-outline"></v-text-field>
            </v-col>
            <v-col cols="2"></v-col>
            <v-col cols="5" offset="1">
              <v-text-field type="number" label="Холодная вода" append-icon="mdi-send-outline"></v-text-field>
            </v-col>
            <v-col cols="2"></v-col>
            <v-col cols="5" offset="1">
              <v-text-field type="number" label="Горячая вода" append-icon="mdi-send-outline"></v-text-field>
            </v-col>
          </v-row>
        </v-card>
      </v-col>
      <v-col lg="8">
        <v-card flat>
          <v-card-title>Статистика за прошлые месяцы</v-card-title>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "CRMCounter"
}
</script>

<style scoped lang="sass">
.counter-wrapper
  padding: 50px
</style>