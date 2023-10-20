extends CanvasLayer

@onready var noiseSeed: LineEdit = $C/H/B/P/V/Noise/V/Seed/LineEdit


func _ready() -> void:
	_random_seed_button_pressed()


func _random_seed_button_pressed() -> void:
	noiseSeed.text = str(randi())
