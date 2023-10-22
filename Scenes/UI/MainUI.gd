extends CanvasLayer

@onready var noiseSeed: LineEdit = $C/H/B/P/S/V/Noise/V/Seed/LineEdit


func _ready() -> void:
	_random_seed_button_pressed()


func _random_seed_button_pressed() -> void:
	noiseSeed.text = str(randi() % 2147483647)
