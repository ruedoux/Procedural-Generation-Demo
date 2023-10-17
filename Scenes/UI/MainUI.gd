extends CanvasLayer

@onready var NoiseEdit: LineEdit = $Control/H/Options/Panel/Sort/Noise/V/Seed/LineEdit
@onready var TypeEdit: OptionButton = $Control/H/Options/Panel/Sort/Noise/V/Type/OptionButton
@onready var OctavesEdit: LineEdit = $Control/H/Options/Panel/Sort/Noise/V/Octaves/LineEdit
@onready var LacunarityEdit: LineEdit = $Control/H/Options/Panel/Sort/Noise/V/Lacunarity/LineEdit
@onready var FrequencyEdit: LineEdit = $Control/H/Options/Panel/Sort/Noise/V/Frequency/LineEdit
@onready var TileRoot: Control = $Control/H/Options/Panel/Sort/Tiles/V/ScrollContainer/V

const TILE_ELEMENT_SCENE = preload("res://Scenes/UI/TileElement.tscn")

var NoiseTypes: Dictionary = {
	"Cellular": FastNoiseLite.TYPE_CELLULAR,
	"Perlin": FastNoiseLite.TYPE_PERLIN,
	"Simplex": FastNoiseLite.TYPE_SIMPLEX,
	"SimplexSmooth": FastNoiseLite.TYPE_SIMPLEX_SMOOTH,
	"Value": FastNoiseLite.TYPE_VALUE,
	"ValueCellular": FastNoiseLite.TYPE_VALUE_CUBIC,
}


func _ready() -> void:
	_random_seed_button_pressed()
	for noiseType in NoiseTypes:
		TypeEdit.add_item(noiseType)


func _generate_pressed() -> void:
	print(NoiseEdit.text)
	print(OctavesEdit.text)
	print(LacunarityEdit.text)
	print(FrequencyEdit.text)


func _random_seed_button_pressed() -> void:
	NoiseEdit.text = str(randi())
